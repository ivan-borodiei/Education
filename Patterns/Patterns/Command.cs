using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Reflection;

namespace Patterns
{
    abstract class Command
    {
        protected ArithmeticDevice arithmeticDevice;
        protected int operand;
        public Command(ArithmeticDevice arithmeticDevice, int operand)
        {
            this.arithmeticDevice = arithmeticDevice;
            this.operand = operand;
        }
        public abstract void Execute();        
    }

    class AddCommand : Command
    {
        public AddCommand(ArithmeticDevice arithmeticDevice, int operand) : base(arithmeticDevice, operand) { }

        public override void Execute()
        {
            arithmeticDevice.Add(operand);
        }
    }

    class ArithmeticDevice
    { 
        public int result;
        public void Add(int val)
        {
            result = result + val;
        }

        public void Sub(int val)
        {
            result = result - val;
        }
    }

    class ManageDevice
    {
        public void Add(Command command)
        {
            command.Execute();            
        }
    }

    class Calculator
    {
        ManageDevice manageDevice = new ManageDevice();
        ArithmeticDevice arithmeticDevice = new ArithmeticDevice();

        public int Add(int value)
        {
            manageDevice.Add(new AddCommand(arithmeticDevice, value));
            return arithmeticDevice.result;
        }
    }
}

namespace Patterns.Additional
{
    class Command : ICommand
    {
        Action<object> action;
        Predicate<object> isEnabled;

        public Command(Action<object> action, Predicate<object> isEnabled)
        {
            this.action = action;
            this.isEnabled = isEnabled;

        }

        public bool CanExecute(object parameter)
        {
            return isEnabled(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            if (CanExecute(parameter) && action != null)
            {
                action(parameter);
                if (CanExecuteChanged != null)
                    CanExecuteChanged(this, null);
            }
        }
    }

    class DescriptionAttribute : Attribute
    {
        public string Description { get; set; }
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }
    }

    public static class EnumExtension
    {
        public static string GetDescription(this Enum num)
        {           
            IEnumerable<MemberInfo> list = num.GetType().GetMembers().Where(x => x.Name == num.ToString());
            foreach (var v in list)
            {
                DescriptionAttribute atr = v.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (atr != null)
                    return atr.Description;
            }
            return num.ToString();
        }
    }

    class RemotePanel
    {        
        const string living = "Гостинная";
        [Flags]
        public enum RemotePanelMode
        {
            [Description("Кухня")]
            Kitchen,
            [Description("Ванная")]
            Bathroom,
            [Description(living)]
            LivingRoom
        }
        public RemotePanelMode Mode { get; set; }

        public ICommand CancelCmd { get; set; }
        public ICommand button1On { get; set; }
        public ICommand button1Off { get; set; }

        public RemotePanel()
        {            
            button1On = new Command((x) => { CancelCmd = button1On; Console.WriteLine("{0} was switched On", Mode.GetDescription()); }, (x) => Mode == RemotePanelMode.LivingRoom);
            button1Off = new Command((x) => { CancelCmd = button1Off; Console.WriteLine("{0} was switched Off", Mode.GetDescription()); }, (x) => Mode == RemotePanelMode.LivingRoom);
            CancelCmd = new Command((x) => { Console.WriteLine("{0} last action was canceled!", Mode.GetDescription()); }, x => CancelCmd != null);
        }
    }

}
