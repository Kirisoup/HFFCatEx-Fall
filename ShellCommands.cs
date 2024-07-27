using System;
using System.Collections.Generic;
using BepInEx;
using HarmonyLib;

public static class ShellCommand {

    private static Lazy<CommandRegistry> CmdReg => new(() => 
        (CommandRegistry)AccessTools
            .Field(typeof(Shell), "commands")
            .GetValue(null)
    );

    public static Lazy<Dictionary<string, Action>> Commands => new(() =>
        (Dictionary<string, Action>)AccessTools
            .Field(typeof(CommandRegistry), "commands")
            .GetValue(CmdReg.Value)
    );    

    public static Lazy<Dictionary<string, Action<string>>> CommandsStr => new(() =>
        (Dictionary<string, Action<string>>)AccessTools
            .Field(typeof(CommandRegistry), "commandsStr")
            .GetValue(CmdReg.Value)
    );

    public static Lazy<string> HelpColor => new(() => 
        (string)AccessTools
            .Field(typeof(CommandRegistry), "helpColor")
            .GetValue(CmdReg.Value)
    );

    public static void Help(string cmd) {
        if (cmd.IsNullOrWhiteSpace()) return;
        CmdReg.Value.OnHelp(cmd);
    }

    public static bool Destroy(string cmd, bool args = false) =>
        args switch {
            false when Commands.Value.Remove(cmd) => true,
            true when CommandsStr.Value.Remove(cmd) => true,
            _ => false
        };
}