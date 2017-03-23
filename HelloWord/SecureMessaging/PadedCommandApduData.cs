﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HelloWord.Infrastructure;

namespace HelloWord.SecureMessaging
{
    public class PadedCommandApduData : IBinary
    {
        private readonly IBinary _commandApduData;
        private readonly byte[] _pad = new byte[] { 0x80, 0x00, 0x00, 0x00, 0x00, 0x00 };

        public PadedCommandApduData(IBinary commandApduData)
        {
            _commandApduData = commandApduData;
        }
        public byte[] Bytes()
        {
            return new ConcatenatedBinaries(
                    _commandApduData,
                    new Binary(_pad)
                ).Bytes();
        }
    }
}

