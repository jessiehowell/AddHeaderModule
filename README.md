# AddHeaderModule
#### An IIS Module to add custom request headers.

##### Purpose:
This module was written to better facilitate using IIS as a reverse proxy. It is often helpful to pass custom headers (especially server variables) to the back end application. Unfortunately, ARR alone can not reliably pass some server variables (i.e. AUTH_USER). The AddHeaderModule will let you quickly add request headers by entering them in you web.config file.

##### Installation:
Install IIS with (at minimum) .NET 4.5 Extensions (I include Windows Auth for reverse proxy):
````
Import-Module ServerManager
Add-WindowsFeature Web-Server, Web-WebServer, Web-Windows-Auth, Web-Net-Ext45, Web-Mgmt-Tools, Web-Mgmt-Console
````
Install ARR 3.0 and Url Rewrite 2:  
https://www.microsoft.com/en-us/download/details.aspx?id=47333  
https://www.iis.net/downloads/microsoft/url-rewrite#additionalDownloads  

Use the Default Web Site or create a new Web Site or Application in IIS, set the App Pool to .NET 4.0, drop the AddHeaderModule.dll into a subfolder named bin in your web site root. Add the AddHeaderModule (https://docs.microsoft.com/en-us/iis/configuration/system.webserver/modules/add) using the name 'AddHeaderModule' and type 'AddHeaderModule.AddHeader'. Optionally, configure URL rewrite (be sure to do this after placing the web.config or it will undo your URL Rewrite settings).

##### Configuration:
Simply use the appSetting section of the web.config (do this by using the Configuration Editor and selecting the appSettings node) to add any custom headers you wish. If you want the value to contain a server variable, then put the variable name between curly braces. IIS server variables can be found here:  
https://msdn.microsoft.com/en-us/library/ms524602%28v=vs.90%29.aspx?f=255&MSPPError=-2147217396

##### Example:
```
<!--Send a header named 'X-Forwarded-User' with the value of the AUTH_USER variable-->
<add key="X-Forwarded-User" value="{AUTH_USER}"/>


<!--Send a header named "My-Header" with value "MyValue"-->
<add key="My-Header" value="MyValue"/>
```

##### Error Handling:
If the module encounters an error with your custom header, a header named "X-Module-Exception" with value of "System.Web" gets set. Usually errors happen when you try to add a non-existant server variable or forget to set a name or value.
