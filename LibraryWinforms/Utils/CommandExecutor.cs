using System.Windows.Input;

namespace LibraryWinforms.Utils
{
    public static class CommandExecutor
    {
        public static void Execute<T>(string commandName, T commandSource, object commandParameter)
        {
            ICommand command = (ICommand)commandSource
                .GetType()
                .GetProperty(commandName)
                .GetValue(commandSource);

            command.Execute(commandParameter);
        }
    }
}
