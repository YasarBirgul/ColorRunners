using Enums;
using UnityEngine;

namespace Controllers
{
    public class DroneAreaColorController : MonoBehaviour
    {

        private MeshRenderer _meshRenderer;
        public ColorType ColorType;
        private void Awake()
        {
            GetReferences();
        }
        private void Start()
        {
            SetGateMaterial(ColorType);
        }
        private void GetReferences()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        public void SetGateMaterial(ColorType colorType)
        {
            _meshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
        }
    }
}

