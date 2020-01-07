# WindowPlacementHelper  
WPF window placement by resolution
  
**NUGET**  
https://www.nuget.org/packages/xCONFLiCTiONx.WindowPlacementHelper/  

**LOGGER**
https://github.com/xCONFLiCTiONx/Logger  
  
**IMPORTANT**  
This is WPF ONLY  
.NET 4.8 is the minimum requirements and I see no reason to set it to anything less.

**FIXES**  
- Changed unecessary math which was reading from the right side of the screen back to the default left.  
- Added upgrade in the dll which I didn't realize was needed until now.  
  
**SUMMARY**  
This is for WPF Windows projects where you would like the window to be in an exact location every time you run the application or the resolution changes.  
  
First time a resolution is used, it will take the location that Windows defaults to and then save that location. If you move the window while that resolution is in use, then it will update that location for that specific resolution.  
  
**Quick Note**  
You can set your windop opacity to 0 and it will not show until after the dll has placed the window in it's saved location. The dll will set the opacity to the given value afterward.  
  ```cs
// 0.5 if you want your window opacity to 0.5 after dll is called and window is placed - Default is 1  
WindowPlacement.SetWindowByResolution(window, false, 0.5);  
```
  
- You must call the dll only after the content has been rendered  
```cs
window.ContentRendered += Window_ContentRendered;

private void Window_ContentRendered(object sender, EventArgs e)
{
	WindowPlacement.SetWindowByResolution(window, false);
}
```

- You can call the dll when the resolution has changed  
```cs
SystemEvents.DisplaySettingsChanged += SystemEvents_DisplaySettingsChanged;

private void SystemEvents_DisplaySettingsChanged(object sender, EventArgs e)
{
	WindowPlacement.SetWindowByResolution(window, false);
}
```

- Save the window placement settings on exit  
```cs
window.Closing += Window_Closing;

private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
{
	WindowPlacement.SetWindowByResolution(window, true);
}
```

- You can also save window location as needed  
```cs
window.MouseUp += Window_MouseUp;

private void Window_MouseUp(object sender, MouseButtonEventArgs e)
{
	WindowPlacement.SetWindowByResolution(window, true);
}
```
  
This is my first dll that i'm making public so if you find errors or have tips please create an issue and i'll look into it .