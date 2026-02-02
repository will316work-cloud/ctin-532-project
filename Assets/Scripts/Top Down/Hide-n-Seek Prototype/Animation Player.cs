using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Container of animation call parameters and animation events for featured animations.
/// 
/// Author: WIlliam Min
/// Date: 12/24/25
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimationPlayer : MonoBehaviour
{
    #region Serialized Fields


    [SerializeField] private AnimationParameters[] _parameters;     // Collection of animation parameters
    [Space] [SerializeField] private UnityEvent[] _animationEvents; // Collection of animation events that the animation states in the animator features


    #endregion

    #region Private Fields


    private Animator _animationController;  // Reference to animator


    #endregion

    #region MonoBehavior Callbacks


    private void Awake()
    {
        _animationController = GetComponent<Animator>();
    }


    #endregion

    #region Public Methods


    /// <summary>
    /// Plays animation with the parameters on the given index.
    /// </summary>
    /// <param name="index">Index of parameters</param>
    public void Play(int index)
    {
        _parameters[index].Play(_animationController);
    }

    /// <summary>
    /// Plays a crossfade transition of the animation with the parameters on the given index.
    /// </summary>
    /// <param name="index">Index of parameters</param>
    public void CrossFade(int index)
    {
        _parameters[index].Crossfade(_animationController);
    }

    /// <summary>
    /// Checks if the animator plays the animation parameters on a given index.
    /// </summary>
    /// <param name="index">Index of animation parameters to check</param>
    /// <returns>Trye if the animator is playing the animation based on given parameters</returns>
    public bool IsPlayingAnimation(int index)
    {
        return _parameters[0].IsPlayingState(_animationController);
    }

    /// <summary>
    /// Checks if the animator is playing any animation.
    /// </summary>
    /// <returns>True if the animator is playing animations</returns>
    public bool IsPlaying()
    {
        foreach (AnimationParameters parameters in _parameters)
        {
            if (parameters.IsPlayingState(_animationController))
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// Executes the events on a given index.
    /// </summary>
    /// <param name="index">Index of collection of events</param>
    public void PlayAnimationEvent(int index)
    {
        _animationEvents[index]?.Invoke();
    }


    #endregion
}

/// <summary>
/// Animation parameters for calling animation states in an animator.
/// 
/// Author: William Min
/// Date: 12/24/25
/// </summary>
[System.Serializable]
public class AnimationParameters
{
    #region Serialized Fields


    [Header("General Animation Play Settings")]
    [SerializeField] private string _stateName;                     // Animation state name
    [SerializeField] private int _layer;                            // Layer number to find animation state
    [SerializeField] private float _normalizedTimeOffset = 0.0f;    // Normalized time offset

    [Header("Crossfade Settings")]
    [SerializeField] private float _normalizedTransitionDuration;       // Normalized transition duration
    [SerializeField] private float _normalizedTransitionTime = 0.0f;    // Normalized transition time


    #endregion

    #region Public Methods


    /// <summary>
    /// Plays the animation with the given parameters on a given animator.
    /// </summary>
    /// <param name="animator">Animator to player animations on</param>
    public void Play(Animator animator)
    {
        animator.Play(_stateName, _layer, _normalizedTimeOffset);
    }

    /// <summary>
    /// Plays a crossfade transition of the animation with the given parameters on a given animator.
    /// </summary>
    /// <param name="animator">Animator to player animations on</param>
    public void Crossfade(Animator animator)
    {
        animator.CrossFade(_stateName, _normalizedTransitionDuration, _layer, _normalizedTimeOffset, _normalizedTransitionTime);
    }

    /// <summary>
    /// Checks if the animator is playing the state matching the parameters.
    /// </summary>
    /// <param name="animator">Animator to check status</param>
    /// <returns>True if the animator is currently playing the animation state featured in the animation parameters</returns>
    public bool IsPlayingState(Animator animator)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(_layer);
        float normailzedTime = stateInfo.normalizedTime;
        bool matchingName = stateInfo.IsName(_stateName);

        //Debug.Log(normailzedTime);
        //Debug.Log(matchingName);

        return matchingName && normailzedTime < 1.0f;
    }


    #endregion
}
