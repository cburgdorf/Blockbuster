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

Be warned, the purpose of this project was to dive into the Reactive Extensions for .NET,
so this is more a demo project instead of anything meant to be serious.

However, because of its Rx(ish) heart it should fit perfectly well for situations where you 
have to deal with directories which held a large number  of files and folders. 
Blockbuster won't load the directory at once, instead it acts in a push based 
reactive manner. 
