using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ATMTuto
{
    public static class FormTransitionHelper
    {
        // 导入 Windows API 用于窗体动画
        [DllImport("user32.dll")]
        private static extern bool AnimateWindow(IntPtr hWnd, int time, AnimateWindowFlags flags);

        [Flags]
        private enum AnimateWindowFlags
        {
            AW_HOR_POSITIVE = 0x00000001,
            AW_HOR_NEGATIVE = 0x00000002,
            AW_VER_POSITIVE = 0x00000004,
            AW_VER_NEGATIVE = 0x00000008,
            AW_CENTER = 0x00000010,
            AW_HIDE = 0x00010000,
            AW_ACTIVATE = 0x00020000,
            AW_SLIDE = 0x00040000,
            AW_BLEND = 0x00080000
        }

        /// <summary>
        /// 淡入显示窗体
        /// </summary>
        public static void FadeIn(Form form, int duration = 300)
        {
            form.Opacity = 0;
            form.Show();
            AnimateWindow(form.Handle, duration, AnimateWindowFlags.AW_BLEND | AnimateWindowFlags.AW_ACTIVATE);
            form.Opacity = 1;
        }

        /// <summary>
        /// 淡出隐藏窗体
        /// </summary>
        public static void FadeOut(Form form, int duration = 300)
        {
            AnimateWindow(form.Handle, duration, AnimateWindowFlags.AW_BLEND | AnimateWindowFlags.AW_HIDE);
            form.Hide();
        }

        /// <summary>
        /// 平滑切换到另一个窗体
        /// </summary>
        public static void SwitchForm(Form currentForm, Form nextForm, int duration = 300)
        {
            FadeOut(currentForm, duration);
            nextForm.StartPosition = FormStartPosition.CenterScreen;
            FadeIn(nextForm, duration);
        }

        /// <summary>
        /// 从中心展开显示窗体
        /// </summary>
        public static void ExpandFromCenter(Form form, int duration = 300)
        {
            form.Show();
            AnimateWindow(form.Handle, duration, AnimateWindowFlags.AW_CENTER | AnimateWindowFlags.AW_ACTIVATE);
        }

        /// <summary>
        /// 向中心收缩隐藏窗体
        /// </summary>
        public static void CollapseToCenter(Form form, int duration = 300)
        {
            AnimateWindow(form.Handle, duration, AnimateWindowFlags.AW_CENTER | AnimateWindowFlags.AW_HIDE);
            form.Hide();
        }
    }
}