﻿using System;

namespace CSShapefile
{
	/// <summary>
	/// Big/little endian byte swapping
	/// </summary>
	public static class Endian
	{
		public static ushort Swap(ushort value)
		{
			return (ushort)((value & 0xFFU) << 8 | (value & 0xFF00U) >> 8);
		}

		public static int Swap(int value)
		{
			return (int)((value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8
						 | (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24);
		}

		public static uint Swap(uint value)
		{
			return (value & 0x000000FFU) << 24 | (value & 0x0000FF00U) << 8
				| (value & 0x00FF0000U) >> 8 | (value & 0xFF000000U) >> 24;
		}

		public static ulong Swap(ulong value)
		{
			return (value & 0x00000000000000FFUL) << 56 | (value & 0x000000000000FF00UL) << 40
				| (value & 0x0000000000FF0000UL) << 24 | (value & 0x00000000FF000000UL) << 8
				| (value & 0x000000FF00000000UL) >> 8 | (value & 0x0000FF0000000000UL) >> 24
				| (value & 0x00FF000000000000UL) >> 40 | (value & 0xFF00000000000000UL) >> 56;
		}
	}
}
