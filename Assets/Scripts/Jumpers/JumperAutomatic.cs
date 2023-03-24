public class JumperAutomatic : JumperControllerBase
{
  protected override void BoxCollided(BoxController boxController)
  {
    base.BoxCollided(boxController);
    JumperStart(eZoneType.Type1, true);
  }
  protected override void ThrowToTarget(BoxController targetBox)
  {
    base.ThrowToTarget(targetBox);
    targetBox.jump(true);
  }
}
