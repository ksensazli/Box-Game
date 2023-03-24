public class JumperAutomatic : JumperControllerBase
{
  protected override void BoxCollided(BoxController boxController)
  {
    base.BoxCollided(boxController);
    JumperStart(eZoneType.Type1, true);
  }
}
