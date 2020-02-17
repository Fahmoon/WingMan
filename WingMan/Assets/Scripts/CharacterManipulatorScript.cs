using UnityEngine;

public class CharacterManipulatorScript : MonoBehaviour
{
    #region Private Fields
    [Header("SET IN INSPECTOR")]
    [SerializeField]
    private GameObject      _normalModel;
    [SerializeField]
    private GameObject      _ragdollModel;
    [SerializeField]
    private float           _forceAfterDeathMagnifier;
    [SerializeField]
    private Rigidbody hips;
    #endregion

    #region MonoBehavior Callbacks
    private void Awake()
    {
        if(_normalModel == null || _ragdollModel == null)
        {
            Debug.LogError("ASSGIN THE MODELS ASS HOLE");
            return;
        }

        _ragdollModel.SetActive(false);

        Rigidbody[] _ragdollsRigidBody = GetComponentsInChildren<Rigidbody>();
        int _counter = _ragdollsRigidBody.Length;

        PhysicMaterial _material = new PhysicMaterial();
        _material.dynamicFriction   = 0.4f;
        _material.staticFriction    = 0.4f;
        _material.bounciness        = 0.5f;

        for (int i = 1; i < _counter; i++)
            _ragdollsRigidBody[i].GetComponent<Collider>().material = _material;
     
    }

    #endregion

    #region Private Methods
    public void ToggleDeath()
    {
            CopyTransformData(_normalModel.transform, _ragdollModel.transform);

            _ragdollModel.SetActive(true);
            _normalModel.SetActive(false);

    }
    private void CopyTransformData(Transform _sourceTransform, Transform _destinationTransform)
    {
        if(_sourceTransform.childCount != _destinationTransform.childCount)
        {
            Debug.LogError("Invalid transform copy, the transform childs doesn't match");
            return;
        }
        int _sourceCounter = _sourceTransform.childCount;

        for (int i = 0; i < _sourceCounter; i++)
        {
            var source = _sourceTransform.GetChild(i);
            var destination = _destinationTransform.GetChild(i);

            destination.position = source.position;
            destination.rotation = source.rotation;
            CopyTransformData(source, destination);
        }
    }
    public void AddForce(Vector3 _collidedWith, Vector3 _collisionPoint)
    {
        Vector3 resultantForce = (hips.transform.position - _collidedWith).normalized;

        hips.AddForceAtPosition(resultantForce * _forceAfterDeathMagnifier,_collisionPoint, ForceMode.Impulse);
    }
    #endregion
}
