using Onnig;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Onnig
{
    public class ScoreDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text _tmpText;

        void Start()
        {

        }

        void Update()
        {
            float scoreTotal = ScoreCollection.PointsOriginality + ScoreCollection.PointsCliche;
            float scoreRatio = scoreTotal > 0 ? (ScoreCollection.PointsOriginality * 100.0f) / scoreTotal : 0f;
            _tmpText.text = $"Progress : {scoreTotal}%\nOriginality : {scoreRatio}%";
            // TODO : implement progress and completion
        }
    }
}
