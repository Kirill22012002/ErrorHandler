
For work use this: 

In Program.cs after authorization and before mapControllers
```
app.UseMiddleware<ExceptionHandlerMiddleware>();
```