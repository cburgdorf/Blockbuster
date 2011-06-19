--------------------------------------
| BLOCKBUSTER                        |
--------------------------------------

Blockbuster is a simple API that makes cleaning up windows directories a breeze.
It was designed to ease server admininstrators pain when it comes to dealing with log files.

At the heart of blockbuster it uses commands to filter files and directories for deletion.
For example, you could use its API like so:


AbstractCommand[] commands = { new FilesOnly(), new KeepLastMonth() };
Blockbuster blockbuster = new Blockbuster();
blockbuster.CleanUp(@"C:\Test", commands);

However, its 2011 and everyone likes fluent APIs. So here you are:

Blockbuster blockbuster = new Blockbuster();
blockbuster
	.WithCommand<FilesOnly>()	//Generic
	.WithCommand(new IsNewer(DateTime.Today.AddMonth(-1))	//Object registration
	.WithCommand(() => IsOlder(DateTime.Today.AddDays(-5))	//Func registration
	.Cleanup(@"C:\Test");



There is also an API for a dynamic configuraton (e.g. XML, Database etc.). You don't even need
to use the library at all if you just want to configure a plant task and throw together
a bunch of useful commands. Use can use the TaskRunner.exe which is a little Console App that can
be configured via XML (see: Blockbuster.Clients.TaskManager.exe.config).

Here is a little XML snippet that shows how to chain some commands via XML:

<?xml version="1.0"?>
<configuration>
  <configSections>
    <section name="CleanUpTasks" type="System.Configuration.NameValueSectionHandler"/>
  </configSections>
  <CleanUpTasks>
    <add key="Command1" value="CommandName='FilesOnly'; Directory = 'C:\Test'"/>
    <add key="Command2" value="CommandName='IsOlder'; Directory = 'C:\Test'; IsOlder='2011-05-05'"/>
    <add key="Command2" value="CommandName='FileExtension'; Directory = 'C:\Test'; FileExtension='log'"/>
  </CleanUpTasks>
  <startup>
    <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.0"/>
  </startup>
</configuration>

So this would search through C:\Test and delete only files (no directories) with the extension .txt that
are older than 2011-05-05.

Be warned, the purpose of this project was to dive into the Reactive Extensions for .NET,
so this is more a demo project instead of anything meant to be serious. 
However, I use it in production and it got its ass backed with a bunch of tests.

However, because of its Rx(ish) heart it should fit perfectly well for situations where you 
have to deal with directories which held a large number  of files and folders. 
Blockbuster won't load the directory at once, instead it acts in a push based 
reactive manner. 
