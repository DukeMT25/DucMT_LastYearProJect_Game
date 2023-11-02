using UnityEngine;
using UnityEngine.EventSystems;

public class UiCardHover : UiBaseCardState
{
    //--------------------------------------------------------------------------------------------------------------

    public UiCardHover(IUiCard handler, BaseStateMachine fsm, UiCardParameters parameters) : base(handler, fsm,
        parameters)
    {
    }

    //--------------------------------------------------------------------------------------------------------------

    void OnPointerExit(PointerEventData obj)
    {
        if (Fsm.IsCurrent(this))
            Handler.Enable();
    }

    void OnPointerDown(PointerEventData eventData)
    {
        if (Fsm.IsCurrent(this))
            Handler.Select();
    }

    //--------------------------------------------------------------------------------------------------------------

    void ResetValues()
    {
        var rotationSpeed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;
        Handler.RotateTo(StartEuler, rotationSpeed);
        Handler.MoveTo(StartPosition, Parameters.HoverSpeed);
        Handler.ScaleTo(StartScale, Parameters.ScaleSpeed);
    }

    void SetRotation()
    {
        if (Parameters.HoverRotation)
            return;

        var speed = Handler.IsPlayer ? Parameters.RotationSpeed : Parameters.RotationSpeedP2;

        Handler.RotateTo(Vector3.zero, speed);
    }

    /// <summary>
    ///     View Math.
    /// </summary>
    void SetPosition()
    {    
        Camera camera = Handler.MainCamera;
        Vector3 halfCardHeight = new Vector3(0, Handler.Renderer.bounds.size.y / 2);
        Vector3 bottomEdge = Handler.MainCamera.ScreenToWorldPoint(Vector3.zero);
        Vector3 topEdge = Handler.MainCamera.ScreenToWorldPoint(new Vector3(0, Screen.height));
        int edgeFactor = Handler.transform.CloserEdge(camera, Screen.width, Screen.height);
        Vector3 myEdge = edgeFactor == 1 ? bottomEdge : topEdge;
        Vector3 edgeY = new Vector3(0, myEdge.y);
        Vector3 currentPosWithoutY = new Vector3(Handler.transform.position.x, 0, Handler.transform.position.z);
        Vector3 hoverHeightParameter = new Vector3(0, Parameters.HoverHeight);
        Vector3 final = currentPosWithoutY + edgeY + (halfCardHeight + hoverHeightParameter) * edgeFactor;
        Handler.MoveTo(final, Parameters.HoverSpeed);
    }
    
    void SetScale()
    {
        var currentScale = Handler.transform.localScale;
        var finalScale = currentScale * Parameters.HoverScale;
        Handler.ScaleTo(finalScale, Parameters.ScaleSpeed);
    }

    void CachePreviousValues()
    {
        StartPosition = Handler.transform.position;
        StartEuler = Handler.transform.eulerAngles;
        StartScale = Handler.transform.localScale;
    }

    void SubscribeInput()
    {
        Handler.Input.OnPointerExit += OnPointerExit;
        Handler.Input.OnPointerDown += OnPointerDown;
    }

    void UnsubscribeInput()
    {
        Handler.Input.OnPointerExit -= OnPointerExit;
        Handler.Input.OnPointerDown -= OnPointerDown;
    }

    void CalcEdge()
    {
    }

    //--------------------------------------------------------------------------------------------------------------

    #region Operations

    public override void OnEnterState()
    {
        MakeRenderFirst();
        SubscribeInput();
        CachePreviousValues();
        SetScale();
        SetPosition();
        SetRotation();
    }

    public override void OnExitState()
    {
        ResetValues();
        UnsubscribeInput();
        DisableCollision();
    }

    #endregion

    //--------------------------------------------------------------------------------------------------------------

    #region Properties

    Vector3 StartPosition { get; set; }
    Vector3 StartEuler { get; set; }
    Vector3 StartScale { get; set; }

    #endregion

    //--------------------------------------------------------------------------------------------------------------
}
