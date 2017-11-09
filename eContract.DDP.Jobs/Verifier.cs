//  
//   Rebex Sample Code License
// 
//   Copyright (c) 2009, Rebex CR s.r.o. www.rebex.net, 
//   All rights reserved.
// 
//   Permission to use, copy, modify, and/or distribute this software for any
//   purpose with or without fee is hereby granted
// 
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
//   OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
//   HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//   WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
//   FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//   OTHER DEALINGS IN THE SOFTWARE.
// 

using System;
using System.Collections;
using System.Text;
using Rebex.Net;
using Rebex.Security.Certificates;

namespace eContract.DDP.Jobs
{
    public class Verifier : Rebex.Net.ICertificateVerifier
    {
        private static Hashtable acceptedCertificates = new Hashtable();

        public TlsCertificateAcceptance Verify(TlsSocket socket, string commonName, CertificateChain certificateChain)
        {
            ValidationResult res = certificateChain.Validate(commonName, 0);

            if (res.Valid)
                return TlsCertificateAcceptance.Accept;

            ValidationStatus status = res.Status;

            ValidationStatus[] values = (ValidationStatus[])Enum.GetValues(typeof(ValidationStatus));

            bool showAddIssuerCaToTrustedCaStore = false;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < values.Length; i++)
            {
                if ((status & values[i]) == 0)
                    continue;

                status ^= values[i];
                string problem;

                switch (values[i])
                {
                    case ValidationStatus.TimeNotValid:
                        problem = "Server certificate has expired or is not valid yet.";
                        break;
                    case ValidationStatus.Revoked:
                        problem = "Server certificate has been revoked.";
                        break;
                    case ValidationStatus.UnknownCa:
                        problem = "Server certificate was issued by an unknown authority.";
                        break;
                    case ValidationStatus.RootNotTrusted:
                        problem = "Server certificate was issued by an untrusted authority.";
                        if (certificateChain.RootCertificate != null)
                            showAddIssuerCaToTrustedCaStore = true;
                        break;
                    case ValidationStatus.IncompleteChain:
                        problem = "Server certificate does not chain up to a trusted root authority.";
                        break;
                    case ValidationStatus.Malformed:
                        problem = "Server certificate is malformed.";
                        break;
                    case ValidationStatus.CnNotMatch:
                        problem = "Server hostname does not match the certificate.";
                        break;
                    case ValidationStatus.UnknownError:
                        problem = string.Format("Error {0:x} encountered while validating server's certificate.", res.ErrorCode);
                        break;
                    default:
                        problem = values[i].ToString();
                        break;
                }

                sb.AppendFormat("{0}\r\n", problem);
            }

            Certificate cert = certificateChain.LeafCertificate;

            string certFingerprint = BitConverter.ToString(cert.GetCertHash());

            // if certificate is already saved than return Accept
            if (acceptedCertificates.Contains(certFingerprint))
                return TlsCertificateAcceptance.Accept;

            VerifierForm certForm = new VerifierForm();
            certForm.Problem = sb.ToString();
            certForm.Hostname = cert.GetCommonName();
            certForm.Subject = cert.GetSubjectName();
            certForm.Issuer = cert.GetIssuerName();
            certForm.ShowAddIssuerToTrustedCaStoreButton = showAddIssuerCaToTrustedCaStore;
            certForm.ValidFrom = cert.GetEffectiveDate().ToString();
            certForm.ValidTo = cert.GetExpirationDate().ToString();

            certForm.ShowDialog();

            // add certificate of the issuer CA to truster authorities store
            if (certForm.AddIssuerCertificateAuthothorityToTrustedCaStore)
            {
                CertificateStore trustedCaStore = new CertificateStore(CertificateStoreName.Root);
                Certificate rootCertificate = certificateChain.RootCertificate;

                trustedCaStore.AddCertificate(rootCertificate);
            }

            if (certForm.Accepted)
            {
                // save accepted certificate for this session in static HashTable
                acceptedCertificates.Add(certFingerprint, cert);
                return TlsCertificateAcceptance.Accept;
            }


            if ((res.Status & ValidationStatus.TimeNotValid) != 0)
                return TlsCertificateAcceptance.Expired;
            if ((res.Status & ValidationStatus.Revoked) != 0)
                return TlsCertificateAcceptance.Revoked;
            if ((res.Status & (ValidationStatus.UnknownCa | ValidationStatus.RootNotTrusted | ValidationStatus.IncompleteChain)) != 0)
                return TlsCertificateAcceptance.UnknownAuthority;
            if ((res.Status & (ValidationStatus.Malformed | ValidationStatus.UnknownError)) != 0)
                return TlsCertificateAcceptance.Other;

            return TlsCertificateAcceptance.Bad;
        }

    }
}
