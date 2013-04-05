// I need invoke webservice operations using standard wsdl, but data objects must be diferent in client and in the server
// Using interfaces for data objects in a common library, making proxy classes for it in client and in server.
// Then, i'm declaring operation contract using the interface, but WCF don't recognise it.
// Someone can help-me? I've attached a complete solution with more details.
// I yet tried use DataContractSerializerBehavior and setting knownTypes, no success

public interface Thing
{
   Guid Id {get;set;}
   String name {get;set;}
   Thing anotherThing {get;set;}
}

[DataContract]
public class ThingAtServer: BsonDocument, Thing // MongoDB persistence
{ 
   [DataMember]
   Guid Id {get;set;}
   //... 
}

[DataContract]
public class ThingAtClient: Thing, INotifyPropertyChanged // WPF bindings
{ 
   [DataMember]
   Guid Id {get;set;}
   //... 
}

[ServiceContract]
public interface MyService
{
  [OperationContract]
  Thing doSomething(Thing input);
}
 