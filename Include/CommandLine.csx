Dictionary<string, string> ParseCommandLineArgs()
{
    var commandLineArgs = new Dictionary<string, string>();
    var args = Environment.GetCommandLineArgs();
    for (var i = 2; i < args.Length; i++)
    {
        var argKeyValuePair = args[i].Split('=');
        if (argKeyValuePair.Length == 1)
        {
            commandLineArgs.Add(argKeyValuePair[0], null);
        }
        else
        {
            commandLineArgs.Add(argKeyValuePair[0], argKeyValuePair[1]);
        }
    }
    return commandLineArgs;
}
