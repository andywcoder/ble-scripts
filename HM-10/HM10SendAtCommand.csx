#load "..\Include\CommandLine.csx"
#load "Include\HM10Device.csx"

var commandLineArgs = ParseCommandLineArgs();
if (!commandLineArgs.ContainsKey("--device-id"))
{
    Console.WriteLine($"Required parameter --device-id is missing");
}
else if (!commandLineArgs.ContainsKey("--at-command"))
{
    Console.WriteLine($"Required parameter --at-command is missing");
}
else
{
    var deviceId = commandLineArgs["--device-id"];
	var atCommand = commandLineArgs["--at-command"];

    await SendAtCommandAsync(deviceId, atCommand, (atCommandResponse) => {
		Console.WriteLine();
		Console.WriteLine($"At command response is {atCommandResponse}");
	});
}
