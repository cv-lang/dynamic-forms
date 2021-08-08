using Cvl.ApplicationServer.Logs.Model;
using Cvl.ApplicationServer.Logs.Storage;
using Cvl.DynamicForms.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cvl.DynamicForms.Test
{
    public class TestDataService : DataServiceBase
    {
        private List<TestPerson> people = new List<TestPerson>();
        private List<Address> addresses = new List<Address>();
        private List<Invoice> invoices = new List<Invoice>();
        private List<Logger> loggers = new List<Logger>();
		private List<LogElement> LogElements = new List<LogElement>();
		private FileLogStorage fileLogStorage;

		private void generate()
        {
            int ilog = 1;
            for (int number = 0; number < 200; number++)
            {
                var tp = new TestPerson() { Id = number, Age = 18 + number % 30, IsEmployed = true, DwellingPlace = Test.Place.Krakow, Earnings = 3.85F };
                people.Add(tp);



                var number3 = number % 3;
                switch (number3)
                {
                    case 0:
                        tp.Firstname = "Jan";
                        tp.BigStringXml = getBigString();
                        break;
                    case 1:
                        tp.Firstname = "Roman";
                        break;
                    case 2:
                        tp.Firstname = "Adam";
                        break;
                }

                var number30 = (number / 3) % 3;
                switch (number30)
                {
                    case 0:
                        tp.Surname = "Kowalski";
                        break;
                    case 1:
                        tp.Surname = "Nowak";
                        break;
                    case 2:
                        tp.Surname = "Kowal";
                        break;
                }



                var address = new Address() { Id = number, City = "Kraków", Street = $"Jana Nowakowskiego {number}", Postcode = "11-222" };
				address.AlternativeAddres = new Address() { City = "a1", AlternativeAddres = new Address() { City = "a2" } };
                addresses.Add(address);
                tp.Address = address;

				tp.Invoices = new HashSet<Invoice>();
                var invoiceCount = Math.Min(Math.Max(3, number), 100);
                for (int i = 0; i < invoiceCount; i++)
                {
                    var invoice = new Invoice() { Id = number * 100 + i, Number = $"{i}/2021", Net = 100 * i, Gross = 123 * 1 };
                    invoices.Add(invoice);
					tp.Invoices.Add(invoice);
                }
				//emulacja entityframeworka
				

                var number10 = number % 10;
                if (number10 == 9)
                {
                    tp.Address = null;
                    tp.Invoices = null;
                }


                var log = new Logger();
                log.Id = ilog++;
                log.Member = "Poziom 0";
                log.Message = $"Message 0 {log.Id}";
                loggers.Add(log);
                for (int i = 0; i < 20; i++)
                {
                    var log2 = new Logger();
                    log2.Id = ilog++;

                    loggers.Add(log2);
                    log2.ParentId = log.Id;
                    log.Subloggers.Add(log2);

                    log2.Member = "Poziom 1";
                    log2.Message = $"Message 1 {log2.Id}";

                    for (int i3 = 0; i3 < 10; i3++)
                    {
                        var log3 = new Logger();
                        log3.Id = ilog++;

                        loggers.Add(log3);
                        log3.ParentId = log2.Id;
                        log2.Subloggers.Add(log3);

                        log3.Member = "Poziom 2";
                        log3.Message = $"Message 2 {log3.Id}";
                    }
                }


            }
        }

        private string getBigString()
        {
            var s = @"<Complex name=''Root'' type=''Test.Processes, Test.Processes''>
	<Properties>
		<Simple name=''State'' value=''CreateContract'' />
		<Complex name=''CompanyVerificationRequest''>
			<Properties>
				<Simple name=''ProcessNumber'' value=''FMKM'' />
				<Simple name=''Nip'' value=''11111'' />
				<Null name=''ClientIpAddress'' />
				<Null name=''ClientIpPort'' />
			</Properties>
		</Complex>
		<Complex name=''CompanyVerificationResponse''>
			<Properties>
				<Simple name=''ResultType'' value=''Success'' />
				<Simple name=''Decision'' type=''Test.Processes.ExternalServices.Engine.EngineService+Decision, Test.Processes.ExternalServices'' value=''Positive'' />
				<Complex name=''Company''>
					<Properties>
						<Simple name=''CompanyName'' value=''Firma &quot;2342&quot; Teest Test'' />
						<Simple name=''PKD'' value=''wer'' />
						<Simple name=''Nip'' value=''333'' />
						<Null name=''KRS'' />
						<Simple name=''REGON'' value=''222'' />
						<Simple name=''LegalFormId'' value=''2'' />
						<Collection name=''People''>
							<Properties>
								<Simple name=''Capacity'' value=''1'' />
							</Properties>
							<Items>
								<Complex>
									<Properties>
										<Null name=''Pesel'' />
										<Null name=''IdNumber'' />
										<Null name=''IdIssueDate'' />
										<Null name=''IdExpiryDate'' />
										<Simple name=''FirstName'' value=''Test'' />
										<Simple name=''Surname'' value=''Test'' />											
										<Null name=''EducationTypeId'' />
										<Null name=''MaritalStatusTypeId'' />
										<Null name=''HouseStatusTypeId'' />
										<Null name=''OtherIncome'' />
										<Null name=''Phone'' />
										<Null name=''Email'' />
										<Null name=''BlueMediaOrderId'' />
										<Collection name=''Consent''>
											<Properties>
												<Simple name=''Capacity'' value=''0'' />
											</Properties>
											<Items />
										</Collection>
										<Simple name=''IsRepresentative'' value=''True'' />
										<Simple name=''IsShareholder'' value=''True'' />
										<Null name=''ClientIpAddress'' />
										<Null name=''ClientIpPort'' />
									</Properties>
								</Complex>
							</Items>
						</Collection>
						<Complex name=''MainAddress''>
							<Properties>
								<Simple name=''Locality'' value='''' />
								<Simple name=''Voivodship'' value=''12'' />
								<Simple name=''PostalCode'' value=''32-777'' />
								<Simple name=''Street'' value=''hhh'' />
								<Simple name=''HouseNumber'' value=''41'' />
								<Null name=''ApartmentNumber'' />
							</Properties>
						</Complex>
						<Complex name=''CorrespondenceAddress''>
							<Properties>
								<Simple name=''Locality'' value=''sdfdf'' />
								<Simple name=''Voivodship'' value=''12'' />
								<Simple name=''PostalCode'' value=''32-540'' />
								<Simple name=''Street'' value=''werer'' />
								<Simple name=''HouseNumber'' value=''41'' />
								<Null name=''ApartmentNumber'' />
							</Properties>
						</Complex>
					</Properties>
				</Complex>
			</Properties>
		</Complex>
		<Complex name=''ProcessApplicationRequestDto''>
			<Properties>
				<Simple name=''ProcessNumber'' value=''4234'' />
				<Complex name=''Application''>
					<Properties>
						<Simple name=''FactorerNip'' value=''6222272'' />
						<Complex name=''Kontomatik''>
							<Properties>
								<Null name=''SessionId'' />
								<Null name=''SessionIdSignature'' />
								<Collection name=''KontomatikXmls''>
									<Properties>
										<Simple name=''Capacity'' value=''4'' />
									</Properties>
									<Items>
										<Complex>
											<Properties>
												<Simple name=''KontomatikXmlBase64'' value=''PHJlc3VsdD4NCiAgPG93bmVycz4NCiAgICA8b3duZXI+DQogICAgICA8bmFtZT5NYW11c3prYSBQYXVsaW5hIEFsY2hpbW93aWN6LWphc2nFhHNrYTwvbmFtZT4NCiAgICAgIDxhZGRyZXNzPsW7RUdMQVJTS0EgNC83VS8yIDExLTUwMCBHScW7WUNLTyBQT0xTS0E8L2FkZHJlc3M+DQogICAgICA8a2luZD5DT01QQU5ZPC9raW5kPg0KICAgIDwvb3duZXI+DQogICAgPG93bmVyPg0KICAgICAgPG5hbWU+UHJ6ZW15c8WCYXcgUnlzemFyZCBKYXNpxYRza2k8L25hbWU+DQogICAgICA8a2luZD5DT19PV05FUjwva2luZD4NCiAgICA8L293bmVyPg0KICAgIDxvd25lcj4NCiAgICAgIDxuYW1lPlBhdWxpbmEgQWxjaGltb3dpY3otamFzacWEc2thPC9uYW1lPg0KICAgICAgPGFkZHJlc3M+xbtFR0xBUlNLQSA0LzdVLzIgMTEtNTAwIEdJxbtZQ0tPIFBPTFNLQSwgV0lMQU5PV1NLQSA1QS83IDExLTUwMCBHScW7WUNLTzwvYWRkcmVzcz4NCiAgICAgIDxwb2xpc2hQZXNlbD45MDA1MTEwMTI2MzwvcG9saXNoUGVzZWw+DQogICAgICA8cGhvbmU+KzQ4NjY2KioqMTI4PC9waG9uZT4NCiAgICAgIDxlbWFpbD5QQVVMSU5BLkFMQ0hJTU9XSUNaQE9ORVQuRVU8L2VtYWlsPg0KICAgICAgPHBlcnNvbmFsR1vdW50Pi0zNy4wMDwvY3VycmVuY3lBbW91bnQ+DQogICAgICAgICAgPGN1cnJlbmN5QmFsYW5jZT41Nzk5Ljk3PC9jdXJyZW5jeUJhbGFuY2U+DQogICAgICAgICAgPHRpdGxlPjc0Mzc4ODMwMzM4MDQ0Mjg1MTk4MzU0LCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMi0wMywgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogMzcsMDAgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODU3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IEdJWllDS08gQWRyZXM6IEtBV0lBUk5JQSBQUk9TVE8gWiBNTFlOPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyBrYXJ0xIU8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMi0wNDwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMi0wNDwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50PjgzLjAwPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjU4MzYuOTc8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+MjRQRyAwOS0yMi4xMS4yMCBTxYJ1xbxieSAwOS0yMi4xMS4yMCBKVyAyNTY4IFpsZWNlbmllMDAzNDM2NzI5ODwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PjI0IFdPSlNLT1dZIE9ERFpJQcWBIEdPU1BPREFSQ1pZIFVMLk5PV09XSUVKU0tBIDIwIDExLTUwMCBHScW7WUNLTzwvcGFydHk+DQogICAgICAgICAgPHBhcnR5SWJhbj5QTDQ5MTAxMDEzOTcwMDIwMTcyMjMwMDAwMDAwPC9wYXJ0eUliYW4+DQogICAgICAgICAgPGtpbmQ+UHJ6ZWxldyBuYSByYWNodW5lazwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTEyLTA0PC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTEyLTA0PC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTIwLjAwPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjU3NTMuOTc8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+UFJaRUxFVyDFmlJPREvDk1csIFJlZmVyZW5jamUgd8WCYXNuZSB6bGVjZW5pb2Rhd2N5OiAxNzIyOTM1OTU2NjU8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT5NQU1VU1pLQSBQQVVMSU5BIEFMQ0hJTU9XSUNaLUpBU0nFg1NLQSBVTC4gxbtFR0xBUlNLQSA0IE0uN1UvMiAxMS01MDBHScW7WUNLIE88L3BhcnR5Pg0KICAgICAgICAgIDxwYXJ0eUliYW4+UEwzNzEwMjA0NzUzMDAwMDA4MDIwMTQzOTYyOTwvcGFydHlJYmFuPg0KICAgICAgICAgIDxraW5kPlByemVsZXcgeiByYWNodW5rdTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTEyLTA0PC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTEyLTA0PC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTYwLjAwPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjU3NzMuOTc8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+T1DFgUFUQSBaQSBGQUtUVVLEmCwgUmVmZXJlbmNqZSB3xYJhc25lIHpsZWNlbmlvZGF3Y3k6IDE3MjI5MzQ2OTQ1ODwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PlA0PC9wYXJ0eT4NCiAgICAgICAgICA8cGFydHlJYmFuPlBMNDIxMDkwMDAwNDc3NzcwMTAwNzU2MjA4OTE8L3BhcnR5SWJhbj4NCiAgICAgICAgICA8a2luZD5QcnplbGV3IHogcmFjaHVua3U8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMi0wNDwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMi0wMjwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50Pi03LjIwPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjU4MzMuOTc8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+NzQzNzg4MzAzMzcwNDQyNTYwNDg1OTcsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTEyLTAyLCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiA3LDIwIFBMTiwgTnVtZXIga2FydHk6IDQyNTEyNSoqKioqKjg4Nzk8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT5LcmFqOiBQT0xTS0EgTWlhc3RvOiBHaXp5Y2tvIEFkcmVzOiBTS0xFUCBFLURBUjwvcGFydHk+DQogICAgICAgICAgPGtpbmQ+UMWCYXRub8WbxIcga2FydMSFPC9raW5kPg0KICAgICAgICAgIDxzdGF0dXM+RE9ORTwvc3RhdHVzPg0KICAgICAgICA8L21vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgIDxtb25leVRyYW5zYWN0aW9uPg0KICAgICAgICAgIDx0cmFuc2FjdGlvbk9uPjIwMjAtMTItMDQ8L3RyYW5zYWN0aW9uT24+DQogICAgICAgICAgPGJvb2tlZE9uPjIwMjAtMTItMDI8L2Jvb2tlZE9uPg0KICAgICAgICAgIDxjdXJyZW5jeUFtb3VudD4tOS44NjwvY3VycmVuY3lBbW91bnQ+DQogICAgICAgICAgPGN1cnJlbmN5QmFsYW5jZT41ODQxLjE3PC9jdXJyZW5jeUJhbGFuY2U+DQogICAgICAgICAgPHRpdGxlPjc0OTg4ODUwMzM3NDk2MzkxNzYwNDEzLCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMi0wMiwgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogOSw4NiBQTE4sIE51bWVyIGthcnR5OiA0MjUxMjUqKioqKio4ODc5PC90aXRsZT4NCiAgICAgICAgICA8cGFydHk+S3JhajogUE9MU0tBIE1pYXN0bzogR0laWUNLTyBBZHJlczogSk1QIFMuQS4gQklFRFJPTktBIDEzMDM8L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTEyLTAzPC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTEyLTAyPC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTEzMy4yNTwvY3VycmVuY3lBbW91bnQ+DQogICAgICAgICAgPGN1cnJlbmN5QmFsYW5jZT41ODUxLjAzPC9jdXJyZW5jeUJhbGFuY2U+DQogICAgICAgICAgPHRpdGxlPk51bWVyIHRlbGVmb251OiArNDggNjY2IDA3MyAxMjggLCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMi0wMiAxMTozNjozMCwgTnVtZXIgcmVmZXJlbmN5am55OiAwMDAwMDA1NTc3OTkzNTI3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PkFkcmVzOiB3d3cubGFtYWdsYW1hLnBsPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyB3ZWIgLSBrb2QgbW9iaWxueTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTEyLTAzPC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTEyLTAxPC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTE2LjgyPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjU5ODQuMjg8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+NzQ5ODg4NTAzMzY0OTYzOTI1MTAyODksIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTEyLTAxLCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiAxNiw4MiBQTE4sIE51bWVyIGthcnR5OiA0MjUxMjUqKioqKio4ODc5PC90aXRsZT4NCiAgICAgICAgICA8cGFydHk+S3JhajogUE9MU0tBIE1pYXN0bzogR0laWUNLTyBBZHJlczogSk1QIFMuQS4gQklFRFJPTktBIDEzMDM8L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTEyLTAzPC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTEyLTAxPC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTEwLjgzPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjYwMDEuMTA8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+NzQ5ODg4NTAzMzY0OTYzMzQxMjAyODgsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTEyLTAxLCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiAxMCw4MyBQTE4sIE51bWVyIGthcnR5OiA0MjUxMjUqKioqKio4ODc5PC90aXRsZT4NCiAgICAgICAgICA8cGFydHk+S3JhajogUE9MU0tBIE1pYXN0bzogR0laWUNLTyBBZHJlczogSk1QIFMuQS4gQklFRFJPTktBIDEzMDM8L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTEyLTAyPC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTEyLTAxPC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTQ5LjE1PC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjYwMTEuOTM8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+MDAwNDgzODQ5IDc0ODM4NDkwMzM2Mjc2Njg4NDMzMDgyLCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMi0wMSAwOToyMDoxNywgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogNDksMTUgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODU3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IEdpenlja28gQWRyZXM6IFJPU1NNQU5OIDAxPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyBrYXJ0xIU8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMi0wMjwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMi0wMTwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50Pi0xMDIuMjc8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NjA2MS4wODwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT4wMDA0ODM4NDkgNzQ4Mzg0OTAzMzYyNzY5MDAzMDM5MzEsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTEyLTAxIDAwOjAwOjAwLCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiAxMDIsMjcgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODU3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IEdJWllDS08gQWRyZXM6IEtBVUZMQU5EPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyBrYXJ0xIU8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMi0wMjwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMS0zMDwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50Pi0yMzguNTA8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NjE2My4zNTwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT4wMTAwNDY1NTEgNzQxNjM1ODAzMzUwMDAxNDUwMTMzMzUsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTExLTMwLCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiAyMzgsNTAgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODU3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IFdBUlNaQVdBIEFkcmVzOiBEUEQgUE9MU0tBIFNQLiBaIE8uTy48L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTEyLTAxPC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTEyLTAxPC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTEyNzkuMDU8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NjQwMS44NTwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT5PUMWBQVpBIFpBIEZBS1RVUsSYIE5SIEZTLzI5MzM3LzEwLzIwMjAsIFJlZmVyZW5jamUgd8WCYXNuZSB6bGVjZW5pb2Rhd2N5OiAxNzIyOTA0MzM5Mzc8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT5BVEVORVVNPC9wYXJ0eT4NCiAgICAgICAgICA8cGFydHlJYmFuPlBMODgxMjQwNDQzMjExMTEwMDAwNDczNDAxNDM8L3BhcnR5SWJhbj4NCiAgICAgICAgICA8a2luZD5QcnplbGV3IHogcmFjaHVua3U8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMi0wMTwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMi0wMTwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50PjM1MTguODI8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NzY4MC45MDwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT4yNFBHIFVQT1MuMTIvMjAgVXBvc2HFvGVuaWUgMTIvMjAgSlcgMjU2OCBabGVjZW5pZTAwMzQyMzYyMTM8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT4yNCBXT0pTS09XWSBPRERaSUHFgSBHT1NQT0RBUkNaWSBVTC5OT1dPV0lFSlNLQSAyMCAxMS01MDAgR0nFu1lDS088L3BhcnR5Pg0KICAgICAgICAgIDxwYXJ0eUliYW4+UEw0OTEwMTAxMzk3MDAyMDE3MjIzMDAwMDAwMDwvcGFydHlJYmFuPg0KICAgICAgICAgIDxraW5kPlByemVsZXcgbmEgcmFjaHVuZWs8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMi0wMTwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMS0yOTwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50Pi0yMDAuMDA8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NDE2Mi4wODwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT4wMTAwNjI5NDEgNzQ5ODc1MDAzMzQwMDAzODU1NTY4MzAsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTExLTI5LCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiAyMDAsMDAgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODU3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IElSTEFORElBIE1pYXN0bzogZmIubWUvYWRzIEFkcmVzOiBGQUNFQksgQUwzNEVZMkhRMjwvcGFydHk+DQogICAgICAgICAgPGtpbmQ+UMWCYXRub8WbxIcga2FydMSFPC9raW5kPg0KICAgICAgICAgIDxzdGF0dXM+RE9ORTwvc3RhdHVzPg0KICAgICAgICA8L21vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgIDxtb25leVRyYW5zYWN0aW9uPg0KICAgICAgICAgIDx0cmFuc2FjdGlvbk9uPjIwMjAtMTEtMzA8L3RyYW5zYWN0aW9uT24+DQogICAgICAgICAgPGJvb2tlZE9uPjIwMjAtMTEtMjg8L2Jvb2tlZE9uPg0KICAgICAgICAgIDxjdXJyZW5jeUFtb3VudD4tMjcuNTk8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NDM2Mi4wODwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT43NDM3ODgzMDMzMzA0NDEzMTcyOTU0OCwgRGF0YSBpIGN6YXMgb3BlcmFjamk6IDIwMjAtMTEtMjgsIE9yeWdpbmFsbmEga3dvdGEgb3BlcmFjamk6IDI3LDU5IFBMTiwgTnVtZXIga2FydHk6IDQyNTEyNSoqKioqKjg4Nzk8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT5LcmFqOiBQT0xTS0EgTWlhc3RvOiBMaWR6YmFyayBXYXJtIEFkcmVzOiBQLkguVS4gV29qY2llY2ggUGxpc3prYTwvcGFydHk+DQogICAgICAgICAgPGtpbmQ+UMWCYXRub8WbxIcga2FydMSFPC9raW5kPg0KICAgICAgICAgIDxzdGF0dXM+RE9ORTwvc3RhdHVzPg0KICAgICAgICA8L21vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgIDxtb25leVRyYW5zYWN0aW9uPg0KICAgICAgICAgIDx0cmFuc2FjdGlvbk9uPjIwMjAtMTEtMjk8L3RyYW5zYWN0aW9uT24+DQogICAgICAgICAgPGJvb2tlZE9uPjIwMjAtMTEtMjc8L2Jvb2tlZE9uPg0KICAgICAgICAgIDxjdXJyZW5jeUFtb3VudD4tMTMuOTc8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NDM4OS42NzwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT4wMDA0OTg4NDkgNzQyMzA3ODAzMzIwODY5NzQwMjU5ODUsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTExLTI3LCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiAxMyw5NyBQTE4sIE51bWVyIGthcnR5OiA0MjUxMjUqKioqKio4ODc5PC90aXRsZT4NCiAgICAgICAgICA8cGFydHk+S3JhajogUE9MU0tBIE1pYXN0bzogR0laWUNLTyBBZHJlczogS0lLIFRFWFRJTCBTUCBaIE8uTy48L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTExLTI5PC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTExLTI3PC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTIwLjk5PC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjQ0MDMuNjQ8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+MDAwNDk4ODQ5IDc0MjMwNzgwMzMyMDg2OTk0MTQzOTMzLCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMS0yNywgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogMjAsOTkgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODg3OTwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IFdFR09SWkVXTyBBZHJlczogQUxLT0hPTEUgU1dJQVRBPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyBrYXJ0xIU8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMS0yOTwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMS0yNzwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50Pi0yNS45ODwvY3VycmVuY3lBbW91bnQ+DQogICAgICAgICAgPGN1cnJlbmN5QmFsYW5jZT40NDI0LjYzPC9jdXJyZW5jeUJhbGFuY2U+DQogICAgICAgICAgPHRpdGxlPjAwMDQ5ODg0OSA3NDIzMDc4MDMzMjA4Njk3NDAyMjUzNywgRGF0YSBpIGN6YXMgb3BlcmFjamk6IDIwMjAtMTEtMjcsIE9yeWdpbmFsbmEga3dvdGEgb3BlcmFjamk6IDI1LDk4IFBMTiwgTnVtZXIga2FydHk6IDQyNTEyNSoqKioqKjg1NzM8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT5LcmFqOiBQT0xTS0EgTWlhc3RvOiBHSVpZQ0tPIEFkcmVzOiBLSUsgVEVYVElMIFNQIFogTy5PLjwvcGFydHk+DQogICAgICAgICAgPGtpbmQ+UMWCYXRub8WbxIcga2FydMSFPC9raW5kPg0KICAgICAgICAgIDxzdGF0dXM+RE9ORTwvc3RhdHVzPg0KICAgICAgICA8L21vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgIDxtb25leVRyYW5zYWN0aW9uPg0KICAgICAgICAgIDx0cmFuc2FjdGlvbk9uPjIwMjAtMTEtMjk8L3RyYW5zYWN0aW9uT24+DQogICAgICAgICAgPGJvb2tlZE9uPjIwMjAtMTEtMjc8L2Jvb2tlZE9uPg0KICAgICAgICAgIDxjdXJyZW5jeUFtb3VudD4tMTUuMTI8L2N1cnJlbmN5QW1vdW50Pg0KICAgICAgICAgIDxjdXJyZW5jeUJhbGFuY2U+NDQ1MC42MTwvY3VycmVuY3lCYWxhbmNlPg0KICAgICAgICAgIDx0aXRsZT43NDk4ODg1MDMzMjQ5NjM5NDUzMDkzMywgRGF0YSBpIGN6YXMgb3BlcmFjamk6IDIwMjAtMTEtMjcsIE9yeWdpbmFsbmEga3dvdGEgb3BlcmFjamk6IDE1LDEyIFBMTiwgTnVtZXIga2FydHk6IDQyNTEyNSoqKioqKjg1NzM8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT5LcmFqOiBQT0xTS0EgTWlhc3RvOiBHSVpZQ0tPIEFkcmVzOiBKTVAgUy5BLiBCSUVEUk9OS0EgNDMzOTwvcGFydHk+DQogICAgICAgICAgPGtpbmQ+UMWCYXRub8WbxIcga2FydMSFPC9raW5kPg0KICAgICAgICAgIDxzdGF0dXM+RE9ORTwvc3RhdHVzPg0KICAgICAgICA8L21vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgIDxtb25leVRyYW5zYWN0aW9uPg0KICAgICAgICAgIDx0cmFuc2FjdGlvbk9uPjIwMjAtMTEtMjg8L3RyYW5zYWN0aW9uT24+DQogICAgICAgICAgPGJvb2tlZE9uPjIwMjAtMTEtMjc8L2Jvb2tlZE9uPg0KICAgICAgICAgIDxjdXJyZW5jeUFtb3VudD4tMTIwLjAwPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjQ0NjUuNzM8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+MDAwNDgzODQ5IDc0ODM4NDkwMzMyMjc2MDIzODEyMTg0LCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMS0yNyAxODo0Mjo0OSwgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogMTIwLDAwIFBMTiwgTnVtZXIga2FydHk6IDQyNTEyNSoqKioqKjg1NzM8L3RpdGxlPg0KICAgICAgICAgIDxwYXJ0eT5LcmFqOiBQT0xTS0EgTWlhc3RvOiBHSVpZQ0tPIEFkcmVzOiBNRSBNMTE8L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTExLTI4PC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTExLTI3PC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTg5Ljk1PC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjQ1ODUuNzM8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+MDAwNDgzODQ5IDc0ODM4NDkwMzMxMjc2MDM0Nzk3Njk4LCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMS0yNyAxOToyMToyNSwgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogODksOTUgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODU3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IEdpenlja28gQWRyZXM6IExJREwgQk9IQVRFUk9XIFdFU1RFUlA8L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTExLTI4PC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTExLTI2PC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTEzLjk5PC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjQ2NzUuNjg8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+NzQ5ODg4NTAzMzE0OTYzODQ3MTAxODEsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTExLTI2LCBPcnlnaW5hbG5hIGt3b3RhIG9wZXJhY2ppOiAxMyw5OSBQTE4sIE51bWVyIGthcnR5OiA0MjUxMjUqKioqKio4ODc5PC90aXRsZT4NCiAgICAgICAgICA8cGFydHk+S3JhajogUE9MU0tBIE1pYXN0bzogR0laWUNLTyBBZHJlczogSk1QIFMuQS4gQklFRFJPTktBIDEzMDM8L3BhcnR5Pg0KICAgICAgICAgIDxraW5kPlDFgmF0bm/Fm8SHIGthcnTEhTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTExLTI3PC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTExLTI2PC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTYxLjUwPC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjQ2ODkuNjc8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+TnVtZXIgdGVsZWZvbnU6ICs0OCA2NjYgMDczIDEyOCAsIERhdGEgaSBjemFzIG9wZXJhY2ppOiAyMDIwLTExLTI2IDE3OjM4OjAzLCBOdW1lciByZWZlcmVuY3lqbnk6IDAwMDAwMDU1NjY2NjIwNTE4PC90aXRsZT4NCiAgICAgICAgICA8cGFydHk+QWRyZXM6IHd3dy5pbnRlcm5ldC1wbHVzLnBsPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyB3ZWIgLSBrb2QgbW9iaWxueTwva2luZD4NCiAgICAgICAgICA8c3RhdHVzPkRPTkU8L3N0YXR1cz4NCiAgICAgICAgPC9tb25leVRyYW5zYWN0aW9uPg0KICAgICAgICA8bW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgICA8dHJhbnNhY3Rpb25Pbj4yMDIwLTExLTI3PC90cmFuc2FjdGlvbk9uPg0KICAgICAgICAgIDxib29rZWRPbj4yMDIwLTExLTI2PC9ib29rZWRPbj4NCiAgICAgICAgICA8Y3VycmVuY3lBbW91bnQ+LTUzLjg3PC9jdXJyZW5jeUFtb3VudD4NCiAgICAgICAgICA8Y3VycmVuY3lCYWxhbmNlPjQ3NTEuMTc8L2N1cnJlbmN5QmFsYW5jZT4NCiAgICAgICAgICA8dGl0bGU+MDAwNDgzODQ5IDc0ODM4NDkwMzMxMjc1NjEwOTY3MzUyLCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMS0yNiAwOToyMjo1MiwgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogNTMsODcgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODU3MzwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IEdpenlja28gQWRyZXM6IFJPU1NNQU5OIDAxPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyBrYXJ0xIU8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMS0yNzwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMS0yNTwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50Pi0xNC4wMDwvY3VycmVuY3lBbW91bnQ+DQogICAgICAgICAgPGN1cnJlbmN5QmFsYW5jZT40ODA1LjA0PC9jdXJyZW5jeUJhbGFuY2U+DQogICAgICAgICAgPHRpdGxlPjc0Mzc4ODMwMzMwMDQzOTY0NDQ1OTQ0LCBEYXRhIGkgY3phcyBvcGVyYWNqaTogMjAyMC0xMS0yNSwgT3J5Z2luYWxuYSBrd290YSBvcGVyYWNqaTogMTQsMDAgUExOLCBOdW1lciBrYXJ0eTogNDI1MTI1KioqKioqODg3OTwvdGl0bGU+DQogICAgICAgICAgPHBhcnR5PktyYWo6IFBPTFNLQSBNaWFzdG86IEdhamV3byBBZHJlczogU2tsZXAgU3Bvenl3Y3pvLVByemVtPC9wYXJ0eT4NCiAgICAgICAgICA8a2luZD5QxYJhdG5vxZvEhyBrYXJ0xIU8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgICAgPG1vbmV5VHJhbnNhY3Rpb24+DQogICAgICAgICAgPHRyYW5zYWN0aW9uT24+MjAyMC0xMS0yNjwvdHJhbnNhY3Rpb25Pbj4NCiAgICAgICAgICA8Ym9va2VkT24+MjAyMC0xMS0yNjwvYm9va2VkT24+DQogICAgICAgICAgPGN1cnJlbmN5QW1vdW50Pi01MC4wMDwvY3VycmVuY3lBbW91bnQ+DQogICAgICAgICAgPGN1cnJlbmN5QmFsYW5jZT40ODE5LjA0PC9jdXJyZW5jeUJhbGFuY2U+DQogICAgICAgICAgPHRpdGxlPlBSWkVMRVcgxZpST0RLw5NXLCBSZWZlcmVuY2plIHfFgmFzbmUgemxlY2VuaW9kYXdjeTogMTcyMjg0OTc0MDY1PC90aXRsZT4NCiAgICAgICAgICA8cGFydHk+UDQgU1AuWk9PPC9wYXJ0eT4NCiAgICAgICAgICA8cGFydHlJYmFuPlBMNDIxMTQwMjEzNDU1NTUwMTAwNjI1ODA1MzA8L3BhcnR5SWJhbj4NCiAgICAgICAgICA8a2luZD5QcnplbGV3IHogcmFjaHVua3U8L2tpbmQ+DQogICAgICAgICAgPHN0YXR1cz5ET05FPC9zdGF0dXM+DQogICAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbj4NCiAgICAgIDwvbW9uZXlUcmFuc2FjdGlvbnM+DQogICAgPC9hY2NvdW50Pg0KICA8L2FjY291bnRzPg0KPC9yZXN1bHQ+'' />
											</Properties>
										</Complex>
									</Items>
								</Collection>
							</Properties>
						</Complex>
						<Null name=''BankStatement'' />
						<Simple name=''Income'' value=''100000'' />
						<Simple name=''ContractorCount'' value=''60'' />
						<Simple name=''IsDeferredPaymentDate'' value=''False'' />
						<Simple name=''BookkeepingTypeId'' value=''1'' />
						<Simple name=''AvgInvoiceMaturity'' value=''0'' />
						<Simple name=''ProposedLimit'' value=''50000'' />
						<Simple name=''BlueMediaOrderId'' value=''1'' />
						<Complex name=''Applicant''>
							<Properties>
								<Simple name=''Pesel'' value=''701103322'' />
								<Simple name=''IdNumber'' value=''VTY752796'' />
								<Simple name=''IdIssueDate'' type=''System.DateTime, System.Private.CoreLib'' value=''03/09/2021 00:00:00'' />
								<Simple name=''IdExpiryDate'' type=''System.DateTime, System.Private.CoreLib'' value=''05/08/2021 00:00:00'' />
								<Simple name=''FirstName'' value=''dss'' />
								<Simple name=''Surname'' value=''www'' />
								<Complex name=''MainAddress'' id=''18''>
									<Properties>
										<Simple name=''Locality'' value=''Wawa'' />
										<Null name=''Voivodship'' />
										<Simple name=''PostalCode'' value=''01-111'' />
										<Simple name=''Street'' value=''al. JP II'' />
										<Simple name=''HouseNumber'' value=''1'' />
										<Null name=''ApartmentNumber'' />
									</Properties>
								</Complex>
								<Reference name=''CorrespondenceAddress'' id=''18'' />
								<Simple name=''EducationTypeId'' type=''System.Int32, System.Private.CoreLib'' value=''2'' />
								<Simple name=''MaritalStatusTypeId'' type=''System.Int32, System.Private.CoreLib'' value=''1'' />
								<Simple name=''HouseStatusTypeId'' type=''System.Int32, System.Private.CoreLib'' value=''1'' />
								<Null name=''OtherIncome'' />
								<Simple name=''Phone'' value=''111222333'' />
								<Simple name=''Email'' value=''test-434@n322m.pl'' />
								<Null name=''BlueMediaOrderId'' />
								<Collection name=''Consent''>
									<Properties>
										<Simple name=''Capacity'' value=''4'' />
									</Properties>
									<Items>
										<Complex>
											<Properties>
												<Simple name=''ConsentId'' value=''1'' />
												<Simple name=''Content'' value=''Zgoda 1 wymagalna'' />
											</Properties>
										</Complex>
									</Items>
								</Collection>
								<Simple name=''IsRepresentative'' value=''False'' />
								<Simple name=''IsShareholder'' value=''False'' />
								<Null name=''ClientIpAddress'' />
								<Null name=''ClientIpPort'' />
							</Properties>
						</Complex>
						<Collection name=''People''>
							<Properties>
								<Simple name=''Capacity'' value=''4'' />
							</Properties>
							<Items>
								<Complex>
									<Properties>
										<Null name=''Pesel'' />
										<Null name=''IdNumber'' />
										<Null name=''IdIssueDate'' />
										<Null name=''IdExpiryDate'' />
										<Simple name=''FirstName'' value=''234234'' />
										<Simple name=''Surname'' value=''234234'' />
										<Null name=''MainAddress'' />
										<Null name=''CorrespondenceAddress'' />
										<Null name=''EducationTypeId'' />
										<Null name=''MaritalStatusTypeId'' />
										<Null name=''HouseStatusTypeId'' />
										<Null name=''OtherIncome'' />
										<Null name=''Phone'' />
										<Null name=''Email'' />
										<Null name=''TestId'' />
										<Collection name=''Consent''>
											<Properties>
												<Simple name=''Capacity'' value=''4'' />
											</Properties>
											<Items>
												<Complex>
													<Properties>
														<Simple name=''ConsentId'' value=''1'' />
														<Simple name=''Content'' value=''bababa'' />
													</Properties>
												</Complex>
											</Items>
										</Collection>
										<Simple name=''IsRepresentative'' value=''False'' />
										<Simple name=''IsShareholder'' value=''False'' />
										<Null name=''ClientIpAddress'' />
										<Null name=''ClientIpPort'' />
									</Properties>
								</Complex>
							</Items>
						</Collection>
						<Collection name=''VendorAdditionalData''>
							<Properties>
								<Simple name=''Capacity'' value=''0'' />
							</Properties>
							<Items />
						</Collection>
					</Properties>
				</Complex>
			</Properties>
		</Complex>
		<Complex name=''ProcessApplicationResponseDto''>
			<Properties>
				<Simple name=''ResultType'' value=''Success'' />
				<Simple name=''Decision'' type=''Test.Processes.ExternalServices.Engine.EngineService+Decision, Test.Processes.ExternalServices'' value=''Positive'' />
				<Collection name=''BankAccountNumbers''>
					<Properties>
						<Simple name=''Capacity'' value=''4'' />
					</Properties>
					<Items>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL581023423400009031354104'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL5312406234234147083244'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL721750117234234809114'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL679176102234234475'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
					</Items>
				</Collection>
				<Simple name=''Limit'' type=''System.Decimal, System.Private.CoreLib'' value=''55555'' />
				<Simple name=''LimitOnline'' type=''System.Decimal, System.Private.CoreLib'' value=''55555'' />
				<Simple name=''DecisionMessage'' value=''werwer'' />
			</Properties>
		</Complex>
		<Complex name=''CreateContractRequestDto''>
			<Properties>
				<Simple name=''ProcessNumber'' value=''fsdfwef'' />
				<Collection name=''BankAccountNumbers''>
					<Properties>
						<Simple name=''Capacity'' value=''4'' />
					</Properties>
					<Items>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL5812342342229031354104'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL53124234234934147083244'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL7217501234234234440809114'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
						<Complex>
							<Properties>
								<Simple name=''AccountNo'' value=''PL679172342344369595760475'' />
								<Simple name=''CurrencyCode'' value=''PLN'' />
							</Properties>
						</Complex>
					</Items>
				</Collection>
			</Properties>
		</Complex>
		<Complex name=''CreateContractResponseDto''>
			<Properties>
				<Simple name=''ResultType'' value=''Pending'' />
			</Properties>
		</Complex>
		<Simple name=''ProcessId'' value=''123'' />
		<Simple name=''ProcessNumber'' value=''234234'' />
	</Properties>
</Complex>﻿";


            return s.Replace("''", "\"");
        }

        public TestDataService()
        {
            generate();
			fileLogStorage = new FileLogStorage("c:\\logs\\");
			LogElements= fileLogStorage.GetHeaders();
        }

        /// <summary>
        /// Zwraca pojedyńczy obiekt
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="typeFullname"></param>
        /// <returns></returns>
        public override object GetObject(string objectId, string typeFullname)
        {
			if (typeFullname == "Cvl.ApplicationServer.Logs.Model.LogElement")
			{
				return fileLogStorage.GetLogElement(objectId);
			}
			else
			{

				var id = int.Parse(objectId);

				switch (typeFullname)
				{
					case "Cvl.DynamicForms.Test.TestPerson":
						return people.FirstOrDefault(x => x.Id == id);
					case "Cvl.DynamicForms.Test.Address":
						return addresses.FirstOrDefault(x => x.Id == id);
					case "Cvl.DynamicForms.Test.Invoice":
						return invoices.FirstOrDefault(x => x.Id == id);
					case "Cvl.DynamicForms.Test.Logger":
						return loggers.FirstOrDefault(x => x.Id == id);
				}
				return null;
			}            
        }

        /// <summary>
        /// Zwraca dzieci obiektu - dla obiektu hierarchicznego
        /// </summary>
        /// <param name="objectId"></param>
        /// <param name="type"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override IQueryable<object> GetChildrenCollection(string objectId, string typeFullname, CollectionViewModelParameters parameters, string mainFilter)
        {
			if (typeFullname == "Cvl.ApplicationServer.Logs.Model.LogElement")
			{
				if(string.IsNullOrEmpty(objectId))
                {
                    if (string.IsNullOrEmpty(mainFilter))
                    {
                        return fileLogStorage.GetHeaders().OrderByDescending(x => x.CreatedDate).Cast<object>().AsQueryable();
                    }
                    else
                    {
						return fileLogStorage.GetHeaders()
                            .Where(x=> (x.MemberName != null && x.MemberName.Contains(mainFilter)) ||
										(x.ExternalId1 != null && x.ExternalId1.Contains(mainFilter)) ||
									   (x.ExternalId2 != null && x.ExternalId2.Contains(mainFilter)) || 
                                       (x.ExternalId3 != null && x.ExternalId3.Contains(mainFilter)) || 
                               (x.ExternalId4 != null && x.ExternalId4.Contains(mainFilter)))
							.OrderByDescending(x => x.CreatedDate)
							.Cast<object>().AsQueryable();
					}
                }

				var log= fileLogStorage.GetLogElement(objectId);
				return log?.Elements.Cast<object>().AsQueryable();
			}

			int? id = null;
            if (!string.IsNullOrEmpty(objectId) && objectId != "null")
            {
                id = int.Parse(objectId);
            }					

			switch (typeFullname)
            {
                case "Cvl.DynamicForms.Test.Logger":
                    return loggers.Where(x => x.ParentId == id).Cast<object>().AsQueryable();
            }

            return null;
        }

        /// <summary>
        /// Zwraca kolekcję obiektów danego obiekt (id)
        /// </summary>
        /// <param name="collectionTypeName"></param>
        /// <param name="objectId"></param>
        /// <param name="objectType"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public override IQueryable<object> GetCollection(string collectionTypeName, string objectId, string objectType, CollectionViewModelParameters parameters)
        {
            int? id = null;
            if (!string.IsNullOrEmpty(objectId) && objectId != "null")
            {
                id = int.Parse(objectId);
            }

            string objectTypeStr = null;
            if (!string.IsNullOrEmpty(objectType) && objectType != "null")
            {
                objectTypeStr = objectType;
            }

            switch (collectionTypeName)
            {
                case "Cvl.DynamicForms.Test.TestPerson":
                    return people.Cast<object>().AsQueryable();
                case "Cvl.DynamicForms.Test.Address":
                    return addresses.Cast<object>().AsQueryable();
                case "Cvl.DynamicForms.Test.Invoice":
                    return invoices.Cast<object>().AsQueryable();
				case "Cvl.ApplicationServer.Logs.Model.LogElement":
					return LogElements.Cast<object>().AsQueryable();

				case "Cvl.DynamicForms.Test.Logger":
                    if (id == null)
                    {
                        return loggers.Cast<object>().AsQueryable();
                    }
                    else
                    {
                        return loggers.Where(x => x.ParentId == id).Cast<object>().AsQueryable();
                    }
            }
            return new List<object>().AsQueryable();
        }

        /// <summary>
        /// Zwraca nazwę propercji Id'ka
        /// </summary>
        /// <param name="valueType"></param>
        /// <returns></returns>
        public override string GetIdPropertyName(Type valueType)
        {
			if(valueType == typeof(Cvl.ApplicationServer.Logs.Model.LogElement))
            {
				return "UniqueId";

			}

            return "Id";
        }
    }
}
