# PEM-utils

[![Build status](https://ci.appveyor.com/api/projects/status/6236saxpg3or6kdl/branch/master?svg=true)](https://ci.appveyor.com/project/huysentruitw/pem-utils/branch/master) [![openupm](https://img.shields.io/npm/v/com.virgis.pem-utils?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.virgis.pem-utils/)

Unity C# utility library for working with PEM files with DER/ASN.1 encoding.

This project has 2 separate libraries:
* DerConverter - for converting ASN.1 syntax from/to binary data
* PemUtils - builds on top of DerConverter for reading/writing [`RSAParameters`](https://msdn.microsoft.com/en-us/library/system.security.cryptography.rsaparameters.aspx) from/to a PEM formatted file

PEM files are commonly used to exchange public or private key data.

Currently files with these headers are supported:

* `----- BEGIN PUBLIC KEY -----` / `----- END PUBLIC KEY -----`
* `----- BEGIN RSA PUBLIC KEY -----` / `----- END RSA PUBLIC KEY -----` (Read Only)
* `----- BEGIN RSA PRIVATE KEY -----` / `----- END RSA PRIVATE KEY -----`

# Install

This is a UPM and can be installed from [OpenUPM](https://openupm.com/packages/com.virgis.pem-utils/) or as a GIT repositoory.

## Usage

### Reading

```C#
using (var stream = File.OpenRead(path))
using (var reader = new PemReader(stream))
{
    var rsaParameters = reader.ReadRsaKey();
    // ...
}
```
 
### Writing

```C#
using (var stream = File.Create(path))
using (var writer = new PemWriter(stream))
{
    // ...
    writer.WritePublicKey(rsaParameters);
}
```
