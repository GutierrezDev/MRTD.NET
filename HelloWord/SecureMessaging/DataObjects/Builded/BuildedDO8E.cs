﻿using System.Linq;
using HelloWord.Infrastructure;
using HelloWord.ISO7816.CommandAPDU.Header;
using HelloWord.SecureMessaging.DataObjects.DO;

namespace HelloWord.SecureMessaging.DataObjects.Builded
{
    public class BuildedDO8E : IBinary
    {
        private readonly IBinary _incrementedSsc;
        private readonly IBinary _rawCommandApdu;
        private readonly IBinary _kSmac;
        private readonly IBinary _kSenc;
        private readonly IBinary _cc;
        public BuildedDO8E(
                IBinary rawCommandApdu,
                IBinary incrementedSsc,
                IBinary kSmac,
                IBinary kSenc
            )
        {
            _incrementedSsc = incrementedSsc;
            _rawCommandApdu = rawCommandApdu;
            _kSmac = kSmac;
            _kSenc = kSenc;
        }
        public byte[] Bytes()
        {
            var cc =
                new CC(
                    _incrementedSsc,
                    _kSmac,
                    new ConcatenatedBinaries(
                        new ProtectedCommandApduHeader(
                            new CommandApduHeader(_rawCommandApdu)
                        ),
                        new BuildedDO87(
                            _rawCommandApdu,
                            _kSenc
                        ),
                        new BuildedDO97(_rawCommandApdu)
                    )
                );

            return new DO8E(cc).Bytes();
        }
    }
}
