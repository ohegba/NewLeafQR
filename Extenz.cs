using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using ZXing.QrCode;

namespace NewLeafQR
{
    static class Extenz
    {
        public static BitArray Prepend(this BitArray current, BitArray before)
        {
            var bools = new bool[current.Count + before.Count];
            before.CopyTo(bools, 0);
            current.CopyTo(bools, before.Count);
            return new BitArray(bools);
        }

        public static BitArray Append(this BitArray current, BitArray after)
        {
            var bools = new bool[current.Count + after.Count];
            current.CopyTo(bools, 0);
            after.CopyTo(bools, current.Count);
            return new BitArray(bools);
        }

        public static byte[] ToByteArray(this BitArray bits)
        {
            int numBytes = bits.Count / 8;
            if (bits.Count % 8 != 0) numBytes++;

            byte[] bytes = new byte[numBytes];
            int byteIndex = 0, bitIndex = 0;

            for (int i = 0; i < bits.Count; i++)
            {
                if (bits[i])
                    bytes[byteIndex] |= (byte)(1 << (7 - bitIndex));

                bitIndex++;
                if (bitIndex == 8)
                {
                    bitIndex = 0;
                    byteIndex++;
                }
            }

            return bytes;
        }

        public static void Reverse(this BitArray array)
        {
            int length = array.Length;
            int mid = (length / 2);

            for (int i = 0; i < mid; i++)
            {
                bool bit = array[i];
                array[i] = array[length - i - 1];
                array[length - i - 1] = bit;
            }
        }

        /*
    public com.google.zxing.common.BitMatrix encode_sa(byte page, byte totalpage, int parity,
        String contents,
        com.google.zxing.BarcodeFormat format,
        int width,
        int height,
        Hashtable hints)  {


    com.google.zxing.qrcode.decoder.ErrorCorrectionLevel errorCorrectionLevel = ErrorCorrectionLevel.L;
    if (hints != null) {
        com.google.zxing.qrcode.decoder.ErrorCorrectionLevel requestedECLevel = (ErrorCorrectionLevel) hints.get(EncodeHintType.ERROR_CORRECTION);
        if (requestedECLevel != null) {
           errorCorrectionLevel = requestedECLevel;
        }
    }

    com.google.zxing.qrcode.encoder.QRCode code = new com.google.zxing.qrcode.encoder.QRCode();
    Encoder.encode_sa(page,totalpage,parity,contents, errorCorrectionLevel, hints, code);
    return renderResult(code, width, height);
}

public static void encode_sa(byte page, byte totalpage, int parity, String content,
    ErrorCorrectionLevel ecLevel,
    Hashtable hints,
    com.google.zxing.qrcode.encoder.QRCode qrCode)  {

    String encoding = hints == null ? null : (String) hints.get(EncodeHintType.CHARACTER_SET);
    if (encoding == null) {
       encoding = com.google.zxing.qrcode.decoder.Mode.BYTE.;
    }

    // Step 1: Choose the mode (encoding).
    Mode mode = com.google.zxing.qrcode.encoder.Encoder.chooseMode(content, encoding);

    // Step 2: Append "bytes" into "dataBits" in appropriate encoding.
      com.google.zxing.common.BitArray  dataBits = new com.google.zxing.common.BitArray(totalpage);

    appendBytes(content, mode, dataBits, encoding);
    // Step 3: Initialize QR code that can contain "dataBits".
    int numInputBits = dataBits.Size;
    com.google.zxing.qrcode.decoder.initQRCode(numInputBits, ecLevel, mode, qrCode);

    // Step 4: Build another bit vector that contains header and data.
    com.google.zxing.common.BitArray headerAndDataBits =   new com.google.zxing.common.BitArray(totalpage);

    // Step 4.1: Add code page info
    byte tmpbyte;
    tmpbyte = 0x3;
    headerAndDataBits.appendBits(tmpbyte, 4);
    headerAndDataBits.appendBits(page, 4);
    headerAndDataBits.appendBits(totalpage, 4);
    headerAndDataBits.appendBits((byte)((parity&0xf0)>>4), 4);
    headerAndDataBits.appendBits((byte)(parity&0x0f), 4);

    appendModeInfo(mode, headerAndDataBits);

    int numLetters = mode == Mode.BYTE ? dataBits.getSizeInBytes() : content.length();
    appendLengthInfo(numLetters, qrCode.getVersion(), mode, headerAndDataBits);
    headerAndDataBits.appendBitArray(dataBits);

    // Step 5: Terminate the bits properly.
    terminateBits(qrCode.getNumDataBytes(), headerAndDataBits);

    // Step 6: Interleave data bits with error correction code.
    BitArray finalBits = new BitArray();
    interleaveWithECBytes(headerAndDataBits, qrCode.getNumTotalBytes(), qrCode.getNumDataBytes(),
    qrCode.getNumRSBlocks(), finalBits);

    // Step 7: Choose the mask pattern and set to "qrCode".
    ByteMatrix matrix = new ByteMatrix(qrCode.getMatrixWidth(), qrCode.getMatrixWidth());
    qrCode.setMaskPattern(chooseMaskPattern(finalBits, ecLevel, qrCode.getVersion(), matrix));

    // Step 8. Build the matrix and set it to "qrCode".
    MatrixUtil.buildMatrix(finalBits, ecLevel, qrCode.getVersion(), qrCode.getMaskPattern(), matrix);
    qrCode.setMatrix(matrix);
    // Step 9. Make sure we have a valid QR Code.
    if (!qrCode.isValid()) {
        throw new WriterException("Invalid QR code: " + qrCode.toString());
    }
}*/


    }
}
