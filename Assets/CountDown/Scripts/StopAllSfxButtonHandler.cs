namespace CountDown
{
    public class StopAllSfxButtonHandler: ButtonClickHandler
    {
        protected override void OnClick()
        {
            SfxManager.Instance.StopAllSounds();
            
        }
    }
}