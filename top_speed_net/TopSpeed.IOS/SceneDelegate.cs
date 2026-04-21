namespace TopSpeed.IOS;

[Register ("SceneDelegate")]
public class SceneDelegate : UIResponder, IUIWindowSceneDelegate {
	private Task? _gameTask;

	[Export ("window")]
	public UIWindow? Window { get; set; }

	[Export ("scene:willConnectToSession:options:")]
	public void WillConnect (UIScene scene, UISceneSession session, UISceneConnectionOptions connectionOptions)
	{
		if (scene is UIWindowScene windowScene) {
			Window ??= new UIWindow (windowScene);

			var vc = new UIViewController ();
			vc.View!.BackgroundColor = UIColor.Black;

			Window.RootViewController = vc;
			Window.MakeKeyAndVisible ();
			StartGame ();
		}
	}

	[Export ("sceneDidDisconnect:")]
	public void DidDisconnect (UIScene scene)
	{
		global::TopSpeed.IOSLauncher.RequestClose ();
	}

	[Export ("sceneDidBecomeActive:")]
	public void DidBecomeActive (UIScene scene)
	{
	}

	[Export ("sceneWillResignActive:")]
	public void WillResignActive (UIScene scene)
	{
	}

	[Export ("sceneWillEnterForeground:")]
	public void WillEnterForeground (UIScene scene)
	{
	}

	[Export ("sceneDidEnterBackground:")]
	public void DidEnterBackground (UIScene scene)
	{
		global::TopSpeed.IOSLauncher.RequestClose ();
	}

	private void StartGame ()
	{
		if (_gameTask != null)
			return;

		global::TopSpeed.IOSLauncher.SetAssetRoot (NSBundle.MainBundle.ResourcePath);
		_gameTask = Task.Run (() => global::TopSpeed.IOSLauncher.Run ());
	}
}
