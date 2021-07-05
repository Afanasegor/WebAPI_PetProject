using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class AttendanceConverter
    {
        public static bool GetAttendanceToBoolean(AttendanceType attendanceType)
        {
            switch (attendanceType)
            {
                case AttendanceType.present:
                    return true;
                case AttendanceType.absant:
                    return false;
                default:
                    throw new ArgumentException("Attendance type doesn't exists");
            }
        }
    }
}
