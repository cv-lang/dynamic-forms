// See https://aka.ms/new-console-template for more information

using Cvl.DynamicForms.Core.SharpSerializer;

Console.WriteLine("Hello, World!");


var xmlTestSerializedData = @"﻿<Complex name=''Root'' type=''Cvl.VirtualMachine.VirtualMachine, Cvl.VirtualMachine, Version=6.0.1.0, Culture=neutral, PublicKeyToken=null'' id=''1''>
  <Properties>
    <Complex name=''Thread'' id=''2''>
      <Properties>
        <Reference name=''WirtualnaMaszyna'' id=''1'' />
        <Complex name=''CallStack''>
          <Properties>
            <Collection name=''StosSerializowany''>
              <Properties>
                <Simple name=''Capacity'' value=''4'' />
              </Properties>
              <Items>
                <Complex>
                  <Properties>
                    <Complex name=''LocalArguments''>
                      <Properties>
                        <Dictionary name=''Obiekty''>
                          <Items>
                            <Item>
                              <Simple value=''0'' />
                              <Complex type=''Cvl.VirtualMachine.Core.Variables.Values.ObjectWraper, Cvl.VirtualMachine, Version=6.0.1.0, Culture=neutral, PublicKeyToken=null''>
                                <Properties>
                                  <Complex name=''Warosc'' type=''Cvl.ApplicationServer.Core.Processes.Services.LongRunningProcessManager, Cvl.ApplicationServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null''>
                                    <Properties>
                                      <Complex name=''Process'' type=''Cvl.ApplicationServer.Test.SimpleLongRunningTestProcess, ConsoleApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'' id=''10''>
                                        <Properties>
                                          <Complex name=''State''>
                                            <Properties>
                                              <Complex name=''Step1Registration''>
                                                <Properties>
                                                  <Simple name=''Email'' value=''test@test.com'' />
                                                  <Simple name=''Password'' value=''sdf'' />
                                                </Properties>
                                              </Complex>
                                              <Simple name=''EmailVerificationCode'' value=''3857'' />
                                            </Properties>
                                          </Complex>
                                        </Properties>
                                      </Complex>
                                    </Properties>
                                  </Complex>
                                </Properties>
                              </Complex>
                            </Item>
                            <Item>
                              <Simple value=''1'' />
                              <Complex type=''Cvl.VirtualMachine.Core.Variables.Values.ObjectWraper, Cvl.VirtualMachine, Version=6.0.1.0, Culture=neutral, PublicKeyToken=null''>
                                <Properties>
                                  <Complex name=''Warosc'' type=''Cvl.ApplicationServer.Core.Processes.UI.View, Cvl.ApplicationServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null'' id=''14''>
                                    <Properties>
                                      <Simple name=''ViewName'' value=''test'' />
                                      <Null name=''ViewDescription'' />
                                      <Null name=''ViewModel'' />
                                      <Collection name=''Actions''>
                                        <Properties>
                                          <Simple name=''Capacity'' value=''0'' />
                                        </Properties>
                                        <Items />
                                      </Collection>
                                    </Properties>
                                  </Complex>
                                </Properties>
                              </Complex>
                            </Item>
                          </Items>
                        </Dictionary>
                      </Properties>
                    </Complex>
                    <Complex name=''LocalVariables''>
                      <Properties>
                        <Dictionary name=''Obiekty''>
                          <Items>
                            <Item>
                              <Simple value=''0'' />
                              <Complex />
                            </Item>
                            <Item>
                              <Simple value=''1'' />
                              <Complex />
                            </Item>
                          </Items>
                        </Dictionary>
                      </Properties>
                    </Complex>
                    <Complex name=''EvaluationStack''>
                      <Properties>
                        <Collection name=''StosSerializowany''>
                          <Properties>
                            <Simple name=''Capacity'' value=''0'' />
                          </Properties>
                          <Items />
                        </Collection>
                      </Properties>
                    </Complex>
                    <Complex name=''TryCatchStack''>
                      <Properties>
                        <Collection name=''TryCatchBlocks''>
                          <Items />
                        </Collection>
                      </Properties>
                    </Complex>
                    <Null name=''ConstrainedType'' />
                    <Simple name=''CzyWykonywacInstrukcje'' value=''False'' />
                    <Simple name=''CzyStatyczna'' value=''False'' />
                    <Simple name=''NazwaTypu'' value=''Cvl.ApplicationServer.Core.Processes.Services.LongRunningProcessManager'' />
                    <Simple name=''NazwaMetody'' value=''WaitForExternalData'' />
                    <Simple name=''MethodFullName'' value=''Cvl.ApplicationServer.Core.Processes.Services.LongRunningProcessManager.WaitForExternalData'' />
                    <Simple name=''AssemblyName'' value=''C:\cvl\projects\application-server\Cvl.ApplicationServer\ConsoleApp1\bin\Debug\net6.0\Cvl.ApplicationServer.dll'' />
                    <Simple name=''Xml'' value=''﻿&lt;Simple name=&quot;Root&quot; type=&quot;System.RuntimeType, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e&quot; value=&quot;Cvl.ApplicationServer.Core.Processes.Services.LongRunningProcessManager, Cvl.ApplicationServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null&quot; /&gt;'' />
                    <Reference name=''WirtualnaMaszyna'' id=''1'' />
                    <Complex name=''NumerWykonywanejInstrukcji''>
                      <Properties>
                        <Collection name=''ExecutionPointsStackSerializowany''>
                          <Properties>
                            <Simple name=''Capacity'' value=''4'' />
                          </Properties>
                          <Items>
                            <Complex>
                              <Properties>
                                <Simple name=''ExecutionInstructionIndex'' value=''12'' />
                              </Properties>
                            </Complex>
                            <Complex>
                              <Properties>
                                <Simple name=''ExecutionInstructionIndex'' value=''0'' />
                              </Properties>
                            </Complex>
                          </Items>
                        </Collection>
                      </Properties>
                    </Complex>
                    <Simple name=''OffsetWykonywanejInstrukcji'' value=''20'' />
                  </Properties>
                </Complex>
                <Complex>
                  <Properties>
                    <Complex name=''LocalArguments''>
                      <Properties>
                        <Dictionary name=''Obiekty''>
                          <Items>
                            <Item>
                              <Simple value=''0'' />
                              <Reference id=''10'' />
                            </Item>
                          </Items>
                        </Dictionary>
                      </Properties>
                    </Complex>
                    <Complex name=''LocalVariables''>
                      <Properties>
                        <Dictionary name=''Obiekty''>
                          <Items>
                            <Item>
                              <Simple value=''0'' />
                              <Complex />
                            </Item>
                            <Item>
                              <Simple value=''1'' />
                              <Complex type=''Cvl.ApplicationServer.Core.Processes.UI.ViewResponse, Cvl.ApplicationServer, Version=6.0.0.0, Culture=neutral, PublicKeyToken=null''>
                                <Properties>
                                  <Null name=''SelectedAction'' />
                                  <Null name=''SelectedObject'' />
                                </Properties>
                              </Complex>
                            </Item>
                            <Item>
                              <Simple value=''2'' />
                              <Simple type=''System.DateTime, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'' value=''2022-03-04T23:09:05.1857529Z'' />
                            </Item>
                            <Item>
                              <Simple value=''3'' />
                              <Complex type=''Cvl.VirtualMachine.VirtualMachineResult`1[[System.Object, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e]], Cvl.VirtualMachine, Version=6.0.1.0, Culture=neutral, PublicKeyToken=null''>
                                <Properties>
                                  <Simple name=''State'' value=''Stoped'' />
                                  <Null name=''Result'' />
                                </Properties>
                              </Complex>
                            </Item>
                          </Items>
                        </Dictionary>
                      </Properties>
                    </Complex>
                    <Complex name=''EvaluationStack''>
                      <Properties>
                        <Collection name=''StosSerializowany''>
                          <Properties>
                            <Simple name=''Capacity'' value=''0'' />
                          </Properties>
                          <Items />
                        </Collection>
                      </Properties>
                    </Complex>
                    <Complex name=''TryCatchStack''>
                      <Properties>
                        <Collection name=''TryCatchBlocks''>
                          <Items />
                        </Collection>
                      </Properties>
                    </Complex>
                    <Null name=''ConstrainedType'' />
                    <Simple name=''CzyWykonywacInstrukcje'' value=''True'' />
                    <Simple name=''CzyStatyczna'' value=''False'' />
                    <Simple name=''NazwaTypu'' value=''Cvl.ApplicationServer.Test.SimpleLongRunningTestProcess'' />
                    <Simple name=''NazwaMetody'' value=''StartLongRunningProcess'' />
                    <Simple name=''MethodFullName'' value=''Cvl.ApplicationServer.Test.SimpleLongRunningTestProcess.StartLongRunningProcess'' />
                    <Simple name=''AssemblyName'' value=''C:\cvl\projects\application-server\Cvl.ApplicationServer\ConsoleApp1\bin\Debug\net6.0\ConsoleApp1.dll'' />
                    <Simple name=''Xml'' value=''﻿&lt;Simple name=&quot;Root&quot; type=&quot;System.RuntimeType, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e&quot; value=&quot;Cvl.ApplicationServer.Test.SimpleLongRunningTestProcess, ConsoleApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null&quot; /&gt;'' />
                    <Reference name=''WirtualnaMaszyna'' id=''1'' />
                    <Complex name=''NumerWykonywanejInstrukcji''>
                      <Properties>
                        <Collection name=''ExecutionPointsStackSerializowany''>
                          <Properties>
                            <Simple name=''Capacity'' value=''4'' />
                          </Properties>
                          <Items>
                            <Complex>
                              <Properties>
                                <Simple name=''ExecutionInstructionIndex'' value=''46'' />
                              </Properties>
                            </Complex>
                            <Complex>
                              <Properties>
                                <Simple name=''ExecutionInstructionIndex'' value=''0'' />
                              </Properties>
                            </Complex>
                            <Complex>
                              <Properties>
                                <Simple name=''ExecutionInstructionIndex'' value=''0'' />
                              </Properties>
                            </Complex>
                          </Items>
                        </Collection>
                      </Properties>
                    </Complex>
                    <Simple name=''OffsetWykonywanejInstrukcji'' value=''155'' />
                  </Properties>
                </Complex>
              </Items>
            </Collection>
          </Properties>
        </Complex>
        <Complex name=''ExceptionHandling''>
          <Properties>
            <Reference name=''VirtualMachine'' id=''1'' />
            <Reference name=''Thread'' id=''2'' />
            <Null name=''ThrowedException'' />
          </Properties>
        </Complex>
        <Simple name=''NumerIteracji'' value=''76'' />
        <Simple name=''Status'' value=''Hibernated'' />
        <Null name=''ThrowedException'' />
        <Null name=''ConstrainedType'' />
        <Collection name=''HibernateParams''>
          <Properties>
            <Simple name=''Capacity'' value=''2'' />
          </Properties>
          <Items>
            <Simple type=''System.Int32, System.Private.CoreLib, Version=6.0.0.0, Culture=neutral, PublicKeyToken=7cec85d7bea7798e'' value=''1'' />
            <Reference id=''14'' />
          </Items>
        </Collection>
        <Null name=''Result'' />
      </Properties>
    </Complex>
    <Reference name=''Instance'' id=''10'' />
    <Simple name=''BreakpointIterationNumber'' value=''-1'' />
    <Null name=''InterpreteFullNameTypes'' />
  </Properties>
</Complex>";

xmlTestSerializedData = xmlTestSerializedData.Replace("''", "\"");

var deserializer = new SharpSerializerXmlToModelDeserializer();
var model = deserializer.DeserializeXml(xmlTestSerializedData);

Console.WriteLine(model);