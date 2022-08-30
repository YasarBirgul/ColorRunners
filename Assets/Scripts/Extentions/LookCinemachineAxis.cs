using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Extentions
{
    [ExecuteInEditMode]
    [SaveDuringPlay]
    [AddComponentMenu("")]
    public class LookCinemachineAxis : CinemachineExtension
    {
        [FormerlySerializedAs("m_XPosition")] [Tooltip("Lock the camera's X position to this value")]
        public float M_XPosition = 0f;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = M_XPosition;
                state.RawPosition = pos;
            }
        }
    }
}