using System.Buffers;

namespace AdventOfCode.Year2015.Day04.Commons;

internal static class MD5Coder
{
	private readonly static int[] _s = [
		7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,  7, 12, 17, 22,
		5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,  5,  9, 14, 20,
		4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,  4, 11, 16, 23,
		6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,  6, 10, 15, 21,
	];

	private readonly static uint[] _k = [
		0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee,
		0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
		0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be,
		0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
		0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa,
		0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
		0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed,
		0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
		0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c,
		0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
		0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05,
		0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
		0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039,
		0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
		0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1,
		0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391,
	];

	public static ReadOnlySpan<char> Code(byte[] input, ArrayPool<byte> poolByte, ArrayPool<uint> poolUint)
	{
		uint a0 = 0x67452301;
		uint b0 = 0xefcdab89;
		uint c0 = 0x98badcfe;
		uint d0 = 0x10325476;

		var addZerosLength = (56 - ((input.Length + 1) % 64)) % 64;
		var processedInput = poolByte.Rent(input.Length + 1 + addZerosLength + 8);
		Array.Copy(input, processedInput, input.Length);
		processedInput[input.Length] = 0x80;

		byte[] length = BitConverter.GetBytes(input.Length * 8);
		Array.Copy(length, 0, processedInput, processedInput.Length - 8, 4);

		for (int index = 0; index < processedInput.Length / 64; ++index)
		{
			uint[] m = poolUint.Rent(16);
			for (int i = 0; i < 16; ++i)
				m[i] = BitConverter.ToUInt32(processedInput, (index * 64) + (i * 4));

			uint a = a0;
			uint b = b0;
			uint c = c0;
			uint d = d0;
			uint f = 0;
			uint g = 0;

			for (uint i = 0; i < 64; ++i)
			{
				if (i <= 15)
				{
					f = (b & c) | (~b & d);
					g = i;
				}
				else if (i >= 16 && i <= 31)
				{
					f = (d & b) | (~d & c);
					g = ((5 * i) + 1) % 16;
				}
				else if (i >= 32 && i <= 47)
				{
					f = b ^ c ^ d;
					g = ((3 * i) + 5) % 16;
				}
				else if (i >= 48)
				{
					f = c ^ (b | ~d);
					g = (7 * i) % 16;
				}

				f = a + f + _k[i] + m[g];
				a = d;
				d = c;
				c = b;
				b += LeftRotate(f, _s[i]);
			}

			poolUint.Return(m);

			a0 += a;
			b0 += b;
			c0 += c;
			d0 += d;
		}

		poolByte.Return(processedInput);

		return GetByteString(a0);  //+ GetByteString(b0) + GetByteString(c0) + GetByteString(d0);
	}

	public static uint LeftRotate(uint x, int c)
	{
		return (x << c) | (x >> (32 - c));
	}

	private static string GetByteString(uint x)
	{
		return String.Join("", BitConverter.GetBytes(x).Select(y => y.ToString("x2")));
	}
}
