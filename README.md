# AddHeaderModule
#### An IIS Module to add custom request headers.

##### Purpose:
This module was written to better facilitate using IIS as a reverse proxy. It is often helpful to pass custom headers (especially server variables) to the back end application. Unfortunately, ARR alone can not reliably pass some server variables (i.e. AUTH_USER). The AddHeaderModule will let you quickly add request headers by entering them in you web.config file.

##### Installation:
Create a new Web Site or Application in IIS, set the App Pool to .NET 4.0, drop the AddHeaderModule.dll into the bin folder of your site and the web.config into the root folder of your site. Optionally, configure URL rewrite (be sure to do this after placing the web.config or it will undo your URL Rewrite settings).

##### Configuration:
Simply use the appSetting section of the web.config to add any custom headers you wish. If you want the value to contain a server variable, then put the variable name between curly braces. IIS server variables can be found here: https://msdn.microsoft.com/en-us/library/ms524602%28v=vs.90%29.aspx?f=255&MSPPError=-2147217396

##### Example:
```
<!--Send a header named 'X-Forwarded-User' with the value of the AUTH_USER variable-->
<add key="X-Forwarded-User" value="{AUTH_USER}"/>


<!--Send a header named "My-Header" with value "MyValue"-->
<add key="My-Header" value="MyValue"/>
```

##### Error Handling:
If the module encounters an error with your custom header, a header named "X-Module-Exception" with value of "System.Web" gets set. Usually errors happen when you try to add a non-existant server variable or forget to set a name or value.
