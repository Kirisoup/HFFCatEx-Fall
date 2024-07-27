using System;
using System.Linq;
using HarmonyLib;
using BepInEx.Configuration;

namespace fall {
        public static class PluginConfig {

        private static ConfigFile configFile;
        private static readonly string cmd = "fall";
        private static readonly string desc = "A master toggle for Fall% mode";

        private static readonly string help = new string[3] { 
            desc,
            "\tUsage: <#F09>fall <arg?></color> ",
            "\tFor a more detailed help please type <#F09>fall help</color> or <#F09>fall h</color>"
        }.Join(delimiter: Environment.NewLine);

        private static readonly string syntaxHelp = new string[6] {
            ShellCommand.HelpColor.Value + "Syntax: <#F09>fall <arg?></color>", 
            "=>  \tWhere <#FFF><#F09><arg></color> represents one of the following actions:</color> ", 
            "    \t<#F90>Toggle:</color>  \t<#F09>`t`</color> or <#F09>`toggle`</color>; ",
            "    \t<#F90>Query:</color>   \t<#F09>`q`</color> or <#F09>`query`</color>; ",
            "    \t<#F90>Enable:</color>  \t<#F09>`1`</color> or <#F09>`on`</color> or <#F09>`enable`</color>; ",
            "    \t<#F90>Disable:</color> \t<#F09>`0`</color> or <#F09>`off`</color> or <#F09>`disable`</color>." + "</color>",
        }.Join(delimiter: Environment.NewLine);

        private static ConfigEntry<bool> enableFall;
        public static bool EnableFall => enableFall.Value;

        public static void Init(ConfigFile config) {
            configFile = config;
            enableFall = configFile.Bind(
                "General", 
                "EnableFall",
                true,
                desc
            );
            Shell.RegisterCommand(cmd, new Action<string>(Toggler), help);
        }

        public static void DestroyCmds() => ShellCommand.Destroy(cmd, true);

        private static void Toggler(string input) {
            bool before = enableFall.Value;
            string[] args = input?.Trim().Split(' ').Select(s => s.Trim()).ToArray() 
                ?? new string[1] { string.Empty };
            try {
                if (args.Length != 1) throw new ArgumentException();
                switch (args[0]) {
                    case "h":
                    case "help":
                        Shell.Print(syntaxHelp); 
                        return;
                    case "q":
                    case "query":
                    case "":
                        break;
                    default:
                        enableFall.Value = args[0] switch {
                            "t" or "toggle" => !enableFall.Value,
                            "1" or "on" or "enable" => true,
                            "0" or "off" or "disable" => false,
                            _ => throw new ArgumentException()
                        };
                        break;
                }
                bool changed = before != enableFall.Value;
                Shell.Print($"Fall%{(changed ? " <#F09>(*Changed)</color>" : string.Empty)}: <#F90>{(enableFall.Value ? "Enabled" : "Disabled")}</color>");
                if (changed) Plugin.OnToggle();
            } catch (ArgumentException) {
                ShellCommand.Help(cmd);
            }
        }
    }
}