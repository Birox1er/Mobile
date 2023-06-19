using System.Collections;
using UnityEngine;

public class FixCamera : MonoBehaviour
{
    public float sceneWidth = 10;
    public float zoomAmount = 2f;
    public float zoomDuration = 1f;
    public float dezoomDuration = 1f;
    public float maxZPosition = -5f;
    public float minHalfHeight = 1f;

    private Camera _camera;
    private float originalSize;
    private bool isZooming;
    private Vector3 originalPosition;
    private float originalOrthographicSize;

    private Coroutine zoomCoroutine;
    private Coroutine dezoomCoroutine;
    private Coroutine followCoroutine;
    private Coroutine followShotCoroutine;
    public bool isZoomed = false;
    public bool isCoroutineFinished;

    private void Start()
    {
        _camera = GetComponent<Camera>();
        originalSize = _camera.orthographicSize;
        originalPosition = _camera.transform.position;
        originalOrthographicSize = _camera.orthographicSize;
    }

    private void Update()
    {
        if (!isZooming && !isCoroutineFinished)
        {
            return; // Wait until the coroutines are finished
        }

        if (!isZooming)
        {
            float unitsPerPixel = sceneWidth / Screen.width;
            float desiredHalfHeight = 0.5f * unitsPerPixel * Screen.height;

            // Ensure the desired half height does not go below the minimum threshold
            desiredHalfHeight = Mathf.Max(desiredHalfHeight, minHalfHeight);
           

        }
    }

    public void ZoomToTarget(Transform target)
    {

        if (zoomCoroutine != null)
        {
            StopCoroutine(zoomCoroutine);
        }
        isCoroutineFinished = false;
        zoomCoroutine = StartCoroutine(ZoomCoroutine(target));
        
    }

    private IEnumerator ZoomCoroutine(Transform target)
    {
        isZoomed = true;
        isZooming = true;
        float targetSize = originalSize / zoomAmount;
        float zoomStartTime = Time.time;
        Vector3 originalPosition = _camera.transform.position;
        float originalOrthographicSize = _camera.orthographicSize;

        while (Time.time - zoomStartTime < zoomDuration)
        {
            float t = (Time.time - zoomStartTime) / zoomDuration;

            // Update camera position to follow the target gradually
            Vector3 targetPosition = target.position;
            targetPosition.z = originalPosition.z; // Keep the original Z position
            _camera.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);

            // Update camera size gradually
            _camera.orthographicSize = Mathf.Lerp(originalOrthographicSize, targetSize, t);
            yield return null;
        }

        isZooming = false;
        zoomCoroutine = null;
        isCoroutineFinished = true;
    }

    public void FollowTarget(Transform target)
    {


        if (followCoroutine != null)
        {
            StopCoroutine(followCoroutine);
        }
        isCoroutineFinished = false;
        followCoroutine = StartCoroutine(FollowCoroutine(target));


    }


    private IEnumerator FollowCoroutine(Transform target)
    {

        float zoomStartTime = Time.time;
        Vector3 originalPosition = _camera.transform.position;

        while (Time.time - zoomStartTime < zoomDuration)
        {
            float t = (Time.time - zoomStartTime) / zoomDuration;

            // Update camera position to follow the target gradually
            Vector3 targetPosition = target.position;
            targetPosition.z = originalPosition.z; // Keep the original Z position
            _camera.transform.position = Vector3.Lerp(originalPosition, targetPosition, t);
            yield return null;

        }
        isCoroutineFinished = true;
        followCoroutine = null;
        

    }

    
    public void FollowShot(GameObject arrow)
    {
        if (followCoroutine != null)
        {
            StopCoroutine(followCoroutine);
        }
        isCoroutineFinished = false;
        followShotCoroutine = StartCoroutine(FollowShotCoroutine(arrow));
        
    }

    private IEnumerator FollowShotCoroutine(GameObject arrow)
    {

        float zoomStartTime = Time.time;
        Vector3 originalPosition = _camera.transform.position;

        while (Time.time - zoomStartTime < zoomDuration)
        {
            float t = (Time.time - zoomStartTime) / zoomDuration;

            // Update camera position to follow the target gradually
            Vector3 targetPosition = arrow.transform.position;
            targetPosition.z = originalPosition.z; // Keep the original Z position
            _camera.transform.position = targetPosition;
            yield return null;
        }

        isCoroutineFinished = true;
        followShotCoroutine = null;

    }

    public void DezoomAndReset()
    {
        if (dezoomCoroutine != null)
        {
            StopCoroutine(dezoomCoroutine);
        }
        isCoroutineFinished = false;
        dezoomCoroutine = StartCoroutine(DezoomCoroutine());
    }

    private IEnumerator DezoomCoroutine()
    {
        isZoomed = false;
        isZooming = true;
        float dezoomStartTime = Time.time;
        float initialOrthographicSize = _camera.orthographicSize;
        Vector3 initialPosition = _camera.transform.position;

        while (Time.time - dezoomStartTime < dezoomDuration)
        {
            float t = (Time.time - dezoomStartTime) / dezoomDuration;

            // Update camera size gradually
            _camera.orthographicSize = Mathf.Lerp(initialOrthographicSize, originalOrthographicSize, t);

            // Update camera position gradually
            _camera.transform.position = Vector3.Lerp(initialPosition, originalPosition, t);
            yield return null;
        }

        _camera.orthographicSize = originalOrthographicSize;
        _camera.transform.position = originalPosition;

        isZooming = false;
        dezoomCoroutine = null;
        isCoroutineFinished = true;
    }
}
