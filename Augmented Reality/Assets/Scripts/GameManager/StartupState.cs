using UnityEngine;

public class StartupState : State
{
    [SerializeField] private StartupMenu startupMenu;
    private ImageTargetHandler marker;

    public override void Activate()
    {
        startupMenu.Show();
        marker = FindObjectOfType<ImageTargetHandler>();
        marker.OnTrackerFound.AddListener(Startup);
    }

    public override void Deactivate()
    {
        startupMenu.Hide();
        marker.OnTrackerFound.RemoveListener(Startup);
    }

    private void Startup()
    {
        stateMachine.GoTo<MenuState>();
    }
}
