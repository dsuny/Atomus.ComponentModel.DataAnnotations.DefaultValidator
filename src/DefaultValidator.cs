using Atomus.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace Atomus.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Atomus.ComponentModel.DataAnnotations.IValidator 인터페이스를 구현한 클래스 입니다.
    /// System.Windows.Controls.Control의 Validator를 적용할 수 있습니다.
    /// </summary>
	#region
    [Author("권대선", 2020, 2, 06, AuthorAttributeType.Create, @"
    /// <summary>
    /// Atomus.ComponentModel.DataAnnotations.IValidator 인터페이스를 구현한 클래스 입니다.
    /// System.Windows.Controls.Control의 Validator를 적용할 수 있습니다.
    /// </summary>")]
    #endregion
    public class DefaultValidator : IValidator
    {
        void IValidator.SetControl(System.Windows.Controls.Control control, string propertyName, UpdateSourceTrigger updateSourceTrigger)
        {
            if (control is TextBox)
            {
                this.SetControl(control as TextBox, propertyName);
                return;
            }


            if (control is PasswordBox)
            {
                this.SetControl(control as PasswordBox, propertyName);
                return;
            }


            if (control is ComboBox)
            {
                this.SetControl(control as ComboBox, propertyName);
                return;
            }

            if (control is CheckBox)
            {
                this.SetControl(control as CheckBox, propertyName);
                return;
            }
        }


        private void SetControl(TextBox control, string propertyName)
        {
            Trigger trigger;
            Style style;

            trigger = new Trigger()
            {
                Property = Validation.HasErrorProperty,
                Value = true
            };

            trigger.Setters.Add(new Setter()
            {
                Property = TextBox.BorderBrushProperty,
                Value = Brushes.Red

            });

            trigger.Setters.Add(new Setter()
            {
                Property = TextBox.ToolTipProperty,
                Value = new Binding()
                {
                    RelativeSource = RelativeSource.Self,
                    Path = new System.Windows.PropertyPath("(Validation.Errors)[0].ErrorContent")
                }
            });

            style = new Style(typeof(TextBox), control.Style);
            style.Triggers.Add(trigger); // Important: add a trigger before applying style
            control.Style = style;

            control.SetBinding(TextBox.TextProperty
                , new Binding()
                {
                    Path = new System.Windows.PropertyPath(propertyName),
                    ValidatesOnDataErrors = true,
                    ValidatesOnExceptions = true,
                    ValidatesOnNotifyDataErrors = true,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });

            Validation.SetErrorTemplate(control, new ControlTemplate());
        }


        private void SetControl(PasswordBox control, string propertyName)
        {
            Trigger trigger;
            Style style;

            trigger = new Trigger()
            {
                Property = Validation.HasErrorProperty,
                Value = true
            };

            trigger.Setters.Add(new Setter()
            {
                Property = PasswordBox.BorderBrushProperty,
                Value = Brushes.Red

            });

            trigger.Setters.Add(new Setter()
            {
                Property = PasswordBox.ToolTipProperty,
                Value = new Binding()
                {
                    RelativeSource = RelativeSource.Self,
                    Path = new System.Windows.PropertyPath("(Validation.Errors)[0].ErrorContent")
                }
            });

            style = new Style(typeof(PasswordBox), control.Style);
            style.Triggers.Add(trigger); // Important: add a trigger before applying style
            control.Style = style;

            control.SetBinding(PasswordBox.PasswordCharProperty
                , new Binding()
                {
                    Path = new System.Windows.PropertyPath(propertyName),
                    ValidatesOnDataErrors = true,
                    ValidatesOnExceptions = true,
                    ValidatesOnNotifyDataErrors = true,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });

            Validation.SetErrorTemplate(control, new ControlTemplate());
        }

        private void SetControl(ComboBox control, string propertyName)
        {
            Trigger trigger;
            Style style;

            trigger = new Trigger()
            {
                Property = Validation.HasErrorProperty,
                Value = true
            };

            trigger.Setters.Add(new Setter()
            {
                Property = ComboBox.BorderBrushProperty,
                Value = Brushes.Red

            });

            trigger.Setters.Add(new Setter()
            {
                Property = ComboBox.ToolTipProperty,
                Value = new Binding()
                {
                    RelativeSource = RelativeSource.Self,
                    Path = new System.Windows.PropertyPath("(Validation.Errors).CurrentItem.ErrorContent}")
                }
            });

            style = new Style(typeof(ComboBox), control.Style);
            style.Triggers.Add(trigger); // Important: add a trigger before applying style
            control.Style = style;

            control.SetBinding(ComboBox.SelectedValueProperty
                , new Binding()
                {
                    Path = new System.Windows.PropertyPath(propertyName),
                    ValidatesOnDataErrors = true,
                    ValidatesOnExceptions = true,
                    ValidatesOnNotifyDataErrors = true,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });

            //Validation.SetErrorTemplate(control, new ControlTemplate());

            control.SelectionChanged += this.Language_SelectionChanged;
        }
        private void Language_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox;
            string tmp;
            ToolTip tooltip;

            comboBox = sender as ComboBox;

            tmp = (comboBox.DataContext as MVVM.ViewModel).OnValidate(BindingOperations.GetBindingExpression(comboBox, ComboBox.SelectedValueProperty).ResolvedSourcePropertyName);

            if (tmp.IsNullOrEmpty())
                comboBox.ToolTip = null;
            else
            {
                tooltip = new ToolTip { Content = tmp };

                comboBox.ToolTip = tooltip;

                //tooltip.Opened += async delegate (object o, RoutedEventArgs args)
                //{
                //    var s = o as ToolTip;

                //    await Task.Delay(1000);
                //    s.IsOpen = false;
                //    comboBox.ToolTip = tmp;
                //    await Task.Delay(1000);
                //};

                //tooltip.VerticalOffset = Mouse.GetPosition(comboBox).Y * -1;
                //tooltip.HorizontalOffset = Mouse.GetPosition(comboBox).X * -1;
                //tooltip.IsOpen = true;
            }
        }


        private void SetControl(CheckBox control, string propertyName)
        {
            Trigger trigger;
            Style style;

            trigger = new Trigger()
            {
                Property = Validation.HasErrorProperty,
                Value = true
            };

            trigger.Setters.Add(new Setter()
            {
                Property = CheckBox.BorderBrushProperty,
                Value = Brushes.Red

            });

            trigger.Setters.Add(new Setter()
            {
                Property = CheckBox.ToolTipProperty,
                Value = new Binding()
                {
                    RelativeSource = RelativeSource.Self,
                    Path = new System.Windows.PropertyPath("(Validation.Errors)[0].ErrorContent")
                }
            });

            style = new Style(typeof(CheckBox), control.Style);
            style.Triggers.Add(trigger); // Important: add a trigger before applying style
            control.Style = style;

            control.SetBinding(CheckBox.IsCheckedProperty
                , new Binding()
                {
                    Path = new System.Windows.PropertyPath(propertyName),
                    ValidatesOnDataErrors = true,
                    ValidatesOnExceptions = true,
                    ValidatesOnNotifyDataErrors = true,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                });

            //Validation.SetErrorTemplate(control, new ControlTemplate());
        }
    }
}