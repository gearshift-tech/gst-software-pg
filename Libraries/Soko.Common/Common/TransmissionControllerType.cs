using System.ComponentModel;

namespace Soko.Common.Common
{
  /// <summary>
  /// Zf6 gearbox model 
  /// </summary>
  public enum GearboxControllerType
  {
    [Description("Non Mechatronic")]
    NON_MECHATRONIC = 0,

    [Description("ZF 6HPxx C_E")]
    ZF_6HPxx_CE = 1,
    [Description("ZF 6HPxx C_M")]
    ZF_6HPxx_CM = 2,
    [Description("ZF 6HPxx 1911_E")]
    ZF_6HPxx_1911E = 3,
    [Description("ZF 6HPxx 1911_M")]
    ZF_6HPxx_1911M = 4,
    [Description("ZF 6HPxx TU_CE")]
    ZF_6HPxx_TUCE = 5,
    [Description("ZF 6HPxx TU_CM")]
    ZF_6HPxx_TUCM = 6,
    [Description("ZF 6HPxx WM")]
    ZF_6HPxx_WM = 11,

    [Description("ZF 8HPxx 1911_E")]
    ZF_8HPxx_1911E = 30,
    // DO NOT USE NUMBERS 12-30 AS THEY ARE RESERVED!!

    [Description("Nissan RE5")]
    NISSAN_RE5 = 31,

    [Description("GM6T40 series")]
    GM6T40 = 32,
    [Description("GM6T70 series")]
    GM6T70 = 33,
    [Description("GML series")]
    GM6L = 34,

    };
}