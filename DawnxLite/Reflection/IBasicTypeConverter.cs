using System;
using System.Reflection;

namespace Dawnx.Reflection
{
    public interface IBasicTypeConverter
    {
        object Convert(PropertyInfo provider, object source);
        object Convert(FieldInfo provider, object source);
        object Convert(Type type, object source, ICustomAttributeProvider provider);

        object ConvertToBoolean(object source, ICustomAttributeProvider provider);
        object ConvertToByte(object source, ICustomAttributeProvider provider);
        object ConvertToChar(object source, ICustomAttributeProvider provider);
        object ConvertToDateTime(object source, ICustomAttributeProvider provider);
        object ConvertToDouble(object source, ICustomAttributeProvider provider);
        object ConvertToInt16(object source, ICustomAttributeProvider provider);
        object ConvertToInt32(object source, ICustomAttributeProvider provider);
        object ConvertToInt64(object source, ICustomAttributeProvider provider);
        object ConvertToSByte(object source, ICustomAttributeProvider provider);
        object ConvertToSingle(object source, ICustomAttributeProvider provider);
        object ConvertToString(object source, ICustomAttributeProvider provider);
        object ConvertToUInt16(object source, ICustomAttributeProvider provider);
        object ConvertToUInt32(object source, ICustomAttributeProvider provider);
        object ConvertToUInt64(object source, ICustomAttributeProvider provider);
    }
}
