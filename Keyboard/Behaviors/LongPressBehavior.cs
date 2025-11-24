using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Keyboard
{
    public class LongPressBehavior : Behavior<Button>
    {
        CancellationTokenSource? _cts;
        Button? _associated;

        public static readonly BindableProperty DurationProperty =
            BindableProperty.Create(nameof(Duration), typeof(int), typeof(LongPressBehavior), 700);

        public int Duration
        {
            get => (int)GetValue(DurationProperty);
            set => SetValue(DurationProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LongPressBehavior), null);

        public ICommand? Command
        {
            get => (ICommand?)GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(LongPressBehavior), null);

        public object? CommandParameter
        {
            get => GetValue(CommandParameterProperty);
            set => SetValue(CommandParameterProperty, value);
        }

        // Optional CLR event if you still want to subscribe in code
        public event EventHandler? LongPressed;

        protected override void OnAttachedTo(Button bindable)
        {
            base.OnAttachedTo(bindable);
            _associated = bindable;
            bindable.Pressed += OnPressed;
            bindable.Released += OnReleased;
            //bindable.Canceled += OnReleased;
        }

        protected override void OnDetachingFrom(Button bindable)
        {
            bindable.Pressed -= OnPressed;
            bindable.Released -= OnReleased;
            //bindable.Canceled -= OnReleased;
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = null;
            _associated = null;
            base.OnDetachingFrom(bindable);
        }

        void OnPressed(object? sender, EventArgs e)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;
            var duration = Duration;

            _ = Task.Run(async () =>
            {
                try
                {
                    await Task.Delay(duration, token);
                    if (!token.IsCancellationRequested)
                    {
                        // Invoke on main thread
                        Microsoft.Maui.ApplicationModel.MainThread.BeginInvokeOnMainThread(() =>
                        {
                            // Raise event
                            LongPressed?.Invoke(_associated, EventArgs.Empty);

                            // Execute command if set
                            if (Command != null && Command.CanExecute(CommandParameter))
                                Command.Execute(CommandParameter);
                        });
                    }
                }
                catch (TaskCanceledException) { }
            }, token);
        }

        void OnReleased(object? sender, EventArgs e)
        {
            _cts?.Cancel();
        }
    }
}