using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FoxBaseUi.ControlEx
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:FoxBaseUi.Button"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:FoxBaseUi.Button;assembly=FoxBaseUi.Button"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:TopImageBtn/>
    ///
    /// </summary>
    public class ImageButton : Button
    {
        static ImageButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        /// <summary>
        /// 按钮类型
        /// </summary>
        public ButtonType ButtonType
        {
            get { return (ButtonType)GetValue(ButtonTypeProperty); }
            set { SetValue(ButtonTypeProperty, value); }
        }

        public static readonly DependencyProperty ButtonTypeProperty =
         DependencyProperty.Register("ButtonType", typeof(ButtonType), typeof(ImageButton), new PropertyMetadata(ButtonType.Normal));


        /// <summary>
        /// 按钮类型
        /// </summary>
        public bool IsBtnSelected
        {
            get { return (bool)GetValue(IsBtnSelectedProperty); }
            set { SetValue(IsBtnSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsBtnSelectedProperty =
         DependencyProperty.Register("IsBtnSelected", typeof(bool), typeof(ImageButton), new PropertyMetadata(false));

        /// <summary>
        /// 图标资源
        /// </summary>
        public ImageSource Icon
        {
            get { return (ImageSource)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty =
         DependencyProperty.Register("Icon", typeof(ImageSource), typeof(ImageButton), new PropertyMetadata(null));

        /// <summary>
        /// 圆角半径
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
         DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ImageButton), new PropertyMetadata(new CornerRadius(0)));


        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty =
         DependencyProperty.Register("MouseOverBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());


        public Brush MousePressedBackground
        {
            get { return (Brush)GetValue(MousePressedBackgroundProperty); }
            set { SetValue(MousePressedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty MousePressedBackgroundProperty =
         DependencyProperty.Register("MousePressedBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());

        public Brush BtnSelectedBackground
        {
            get { return (Brush)GetValue(BtnSelectedBackgroundProperty); }
            set { SetValue(BtnSelectedBackgroundProperty, value); }
        }

        public static readonly DependencyProperty BtnSelectedBackgroundProperty =
         DependencyProperty.Register("BtnSelectedBackground", typeof(Brush), typeof(ImageButton), new PropertyMetadata());
    }

    /// <summary>
    /// 按钮类型
    /// </summary>
    public enum ButtonType
    {
        //扁平化按钮，无图标
        Normal,
        //只有图标，无文字
        OnlyIcon,
        //图标在按钮上方
        TopIcon,
        //图标在按钮左边
        LeftIcon,
        //图标在按钮右边
        RightIcon,
        //图标在按钮下方
        BottomIcon
    }

}
