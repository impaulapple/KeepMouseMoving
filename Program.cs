using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading;

namespace KeepMouseMoving
{
  class Program
  {
    private static Timer _Timer = null;
    private static int _LAST_X = 0;
    private static int _LAST_Y = 0;
    private static int _IS_SAME_XY_COUNTER = 0;

    static void Main(string[] args)
    {
      Console.WriteLine("Start automatic mouse movement.");
      _Timer = new Timer(TimerCallback, null, 0, 2000);
      Console.ReadLine();
    }


    private static void TimerCallback(object o)
    {
      var addRange = 100;
      var depnt = new Point();
      GetCursorPos(ref depnt);
      Console.WriteLine($"X:{depnt.X} ,Y:{depnt.Y}");
      var oldX = depnt.X;
      var oldY = depnt.Y;
      if (!oldX.Equals(_LAST_X) || !oldY.Equals(_LAST_Y))
      {
        _LAST_X = oldX;
        _LAST_Y = oldY;
        return;
      }

      SetCursorPos(oldX + addRange, oldY + addRange);

      GetCursorPos(ref depnt);
      _LAST_X = depnt.X;
      _LAST_Y = depnt.Y;

      // 到右下角的底了
      if (oldX.Equals(_LAST_X) && oldY.Equals(_LAST_Y))
      {
        _IS_SAME_XY_COUNTER++;
      }
      else
      {
        _IS_SAME_XY_COUNTER = 0;
      }

      if(_IS_SAME_XY_COUNTER > 2)
      {
        SetCursorPos(0, 0);
      }
    }

    [DllImport("user32.dll")]
    static extern bool GetCursorPos(ref Point lpPoint);

    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);

  }
}
