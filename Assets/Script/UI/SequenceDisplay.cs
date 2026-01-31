using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MoreMountains.Feedbacks;
using NUnit.Framework;
using Script.GameColor;
using TMPro;
using UnityEngine;

namespace Script.UI
{
    public class SequenceDisplay : MonoBehaviour
    {
        struct DisplaySlotState
        {
            public bool bActive;
            public string Text;
            public Color Color;
        }

        [SerializeField]
        MMF_Player _wrongShakeFeedback;

        [SerializeField]
        ColorDict _colorDict;

        [SerializeField]
        private List<TextMeshProUGUI> _sequenceTexts = new List<TextMeshProUGUI>();

        private List<DisplaySlotState> _savedDisplaySlotStates = new List<DisplaySlotState>();

        int _currentActiveIndex = 0;

        private bool bDisplayMutable = true;

        private void Awake()
        {
            _currentActiveIndex = 0;

            foreach(var text in _sequenceTexts)
            {
                text.text = "";
                text.color = _colorDict.GetColor(GameColorEnum.White);
                text.gameObject.SetActive(false);
            }

            SaveDisplayState();
        }

        public void OnReceiveMovementInputResult(MovementResult mr)
        {
            UpdateDisplayTask(mr);
        }

        private void EnableRightMostSlot(string displaySymbol, Color displayColor)
        {
            var text = _sequenceTexts[_currentActiveIndex];
            text.gameObject.SetActive(true);

            text.text = displaySymbol;
            text.color = displayColor;

            _currentActiveIndex = Mathf.Min(_currentActiveIndex + 1, _sequenceTexts.Count);
        }

        private void SaveDisplayState()
        {
            _savedDisplaySlotStates.Clear();
            foreach(var text in _sequenceTexts)
            {
                _savedDisplaySlotStates.Add(new DisplaySlotState()
                {
                    bActive = text.gameObject.activeSelf,
                    Text = text.text,
                    Color = text.color
                });
            }
        }

        private void LoadDisplayState()
        {
            for(int i = 0; i < _sequenceTexts.Count; i++)
            {
                var text = _sequenceTexts[i];
                var savedState = _savedDisplaySlotStates[i];
                text.gameObject.SetActive(savedState.bActive);
                text.text = savedState.Text;
                text.color = savedState.Color;
            }
        }

        private void ShiftDisplay()
        {
            for(int i = 0; i < _sequenceTexts.Count - 1; i++)
            {
                _sequenceTexts[i].text = _sequenceTexts[i + 1].text;
                _sequenceTexts[i].color = _sequenceTexts[i + 1].color;
            }

            DisableLastSlot();
        }

        private void DisableLastSlot()
        {
            // Disable the last one
            var lastText = _sequenceTexts[^1];
            _currentActiveIndex = _sequenceTexts.Count - 1;
            lastText.text = "";
            lastText.gameObject.SetActive(false);
        }

        private async void UpdateDisplayTask(MovementResult mr)
        {
            while(!bDisplayMutable)
            {
                if(_wrongShakeTask != null && !_wrongShakeTask.IsCanceled)
                {
                    try
                    {
                        _wrongShakeTask.Dispose();
                        _wrongShakeFeedback.SkipToTheEnd();
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    await Task.Yield();
                }
            }

            if(_currentActiveIndex >= _sequenceTexts.Count)
            {
                // Shift all texts to the left
                ShiftDisplay();
            }

            string displaySymbol = mr.InputSequence.ToDisplayString();
            Color displayColor = _colorDict.GetColor(mr.NextColor);

            EnableRightMostSlot(displaySymbol, displayColor);

            if(!mr.bCanMove)
            {
                PlayWrongShakeFeedback();
            }
            else
            {
                SaveDisplayState();
            }
        }

        Task _wrongShakeTask = null;

        private async void PlayWrongShakeFeedback()
        {
            bDisplayMutable = false;
            if(_wrongShakeFeedback != null)
            {
                _wrongShakeTask = _wrongShakeFeedback.PlayFeedbacksTask();
                await _wrongShakeTask;
                _wrongShakeTask = null;

                LoadDisplayState();
            }

            bDisplayMutable = true;
        }
    }
}