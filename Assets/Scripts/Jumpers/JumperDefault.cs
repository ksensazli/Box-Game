public class JumperDefault : JumperControllerBase
{
    
    protected override void OnEnable()
    {
        base.OnEnable();
        JumpButton.OnJumpButton += OnJumpButton;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        JumpButton.OnJumpButton -= OnJumpButton;
    }
    protected override void ThrowToTarget(BoxController targetBox)
    {
        base.ThrowToTarget(targetBox);
        targetBox.jump(false);
    }
}

