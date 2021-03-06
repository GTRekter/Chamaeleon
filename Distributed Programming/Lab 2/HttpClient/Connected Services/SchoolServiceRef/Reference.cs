//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HttpClient.SchoolServiceRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="GenderEnum", Namespace="http://schemas.datacontract.org/2004/07/Lab2Service.Models")]
    public enum GenderEnum : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unknown = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Male = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Female = 2,
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Person", Namespace="http://schemas.datacontract.org/2004/07/Lab2Service.Models")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HttpClient.SchoolServiceRef.Teacher))]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(HttpClient.SchoolServiceRef.Student))]
    public partial class Person : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DOBField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string FirstNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private HttpClient.SchoolServiceRef.GenderEnum GenderField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LastNameField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DOB {
            get {
                return this.DOBField;
            }
            set {
                if ((this.DOBField.Equals(value) != true)) {
                    this.DOBField = value;
                    this.RaisePropertyChanged("DOB");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string FirstName {
            get {
                return this.FirstNameField;
            }
            set {
                if ((object.ReferenceEquals(this.FirstNameField, value) != true)) {
                    this.FirstNameField = value;
                    this.RaisePropertyChanged("FirstName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public HttpClient.SchoolServiceRef.GenderEnum Gender {
            get {
                return this.GenderField;
            }
            set {
                if ((this.GenderField.Equals(value) != true)) {
                    this.GenderField = value;
                    this.RaisePropertyChanged("Gender");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LastName {
            get {
                return this.LastNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LastNameField, value) != true)) {
                    this.LastNameField = value;
                    this.RaisePropertyChanged("LastName");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Teacher", Namespace="http://schemas.datacontract.org/2004/07/Lab2Service.Models")]
    [System.SerializableAttribute()]
    public partial class Teacher : HttpClient.SchoolServiceRef.Person {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateOfHireField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int SalaryField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime DateOfHire {
            get {
                return this.DateOfHireField;
            }
            set {
                if ((this.DateOfHireField.Equals(value) != true)) {
                    this.DateOfHireField = value;
                    this.RaisePropertyChanged("DateOfHire");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int Salary {
            get {
                return this.SalaryField;
            }
            set {
                if ((this.SalaryField.Equals(value) != true)) {
                    this.SalaryField = value;
                    this.RaisePropertyChanged("Salary");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Student", Namespace="http://schemas.datacontract.org/2004/07/Lab2Service.Models")]
    [System.SerializableAttribute()]
    public partial class Student : HttpClient.SchoolServiceRef.Person {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float GPAField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MajorField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private float UnitsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float GPA {
            get {
                return this.GPAField;
            }
            set {
                if ((this.GPAField.Equals(value) != true)) {
                    this.GPAField = value;
                    this.RaisePropertyChanged("GPA");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ID {
            get {
                return this.IDField;
            }
            set {
                if ((object.ReferenceEquals(this.IDField, value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Major {
            get {
                return this.MajorField;
            }
            set {
                if ((object.ReferenceEquals(this.MajorField, value) != true)) {
                    this.MajorField = value;
                    this.RaisePropertyChanged("Major");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public float Units {
            get {
                return this.UnitsField;
            }
            set {
                if ((this.UnitsField.Equals(value) != true)) {
                    this.UnitsField = value;
                    this.RaisePropertyChanged("Units");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="PersonList", Namespace="http://schemas.datacontract.org/2004/07/Lab2Service.Models", ItemName="Person")]
    [System.SerializableAttribute()]
    public class PersonList : System.Collections.Generic.List<HttpClient.SchoolServiceRef.Person> {
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SchoolServiceRef.ISchoolService")]
    public interface ISchoolService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/AddStudent", ReplyAction="http://tempuri.org/ISchoolService/AddStudentResponse")]
        HttpClient.SchoolServiceRef.Student AddStudent(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/AddStudent", ReplyAction="http://tempuri.org/ISchoolService/AddStudentResponse")]
        System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Student> AddStudentAsync(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/DeleteStudent", ReplyAction="http://tempuri.org/ISchoolService/DeleteStudentResponse")]
        void DeleteStudent(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/DeleteStudent", ReplyAction="http://tempuri.org/ISchoolService/DeleteStudentResponse")]
        System.Threading.Tasks.Task DeleteStudentAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetStudent", ReplyAction="http://tempuri.org/ISchoolService/GetStudentResponse")]
        HttpClient.SchoolServiceRef.Student GetStudent(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetStudent", ReplyAction="http://tempuri.org/ISchoolService/GetStudentResponse")]
        System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Student> GetStudentAsync(string id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetStudents", ReplyAction="http://tempuri.org/ISchoolService/GetStudentsResponse")]
        System.Collections.Generic.List<HttpClient.SchoolServiceRef.Student> GetStudents();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetStudents", ReplyAction="http://tempuri.org/ISchoolService/GetStudentsResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<HttpClient.SchoolServiceRef.Student>> GetStudentsAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/UpdateStudent", ReplyAction="http://tempuri.org/ISchoolService/UpdateStudentResponse")]
        HttpClient.SchoolServiceRef.Student UpdateStudent(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/UpdateStudent", ReplyAction="http://tempuri.org/ISchoolService/UpdateStudentResponse")]
        System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Student> UpdateStudentAsync(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/AddTeacher", ReplyAction="http://tempuri.org/ISchoolService/AddTeacherResponse")]
        HttpClient.SchoolServiceRef.Teacher AddTeacher(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/AddTeacher", ReplyAction="http://tempuri.org/ISchoolService/AddTeacherResponse")]
        System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Teacher> AddTeacherAsync(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/DeleteTeacher", ReplyAction="http://tempuri.org/ISchoolService/DeleteTeacherResponse")]
        void DeleteTeacher(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/DeleteTeacher", ReplyAction="http://tempuri.org/ISchoolService/DeleteTeacherResponse")]
        System.Threading.Tasks.Task DeleteTeacherAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetTeacher", ReplyAction="http://tempuri.org/ISchoolService/GetTeacherResponse")]
        HttpClient.SchoolServiceRef.Teacher GetTeacher(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetTeacher", ReplyAction="http://tempuri.org/ISchoolService/GetTeacherResponse")]
        System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Teacher> GetTeacherAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetTeachers", ReplyAction="http://tempuri.org/ISchoolService/GetTeachersResponse")]
        System.Collections.Generic.List<HttpClient.SchoolServiceRef.Teacher> GetTeachers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetTeachers", ReplyAction="http://tempuri.org/ISchoolService/GetTeachersResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<HttpClient.SchoolServiceRef.Teacher>> GetTeachersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/UpdateTeacher", ReplyAction="http://tempuri.org/ISchoolService/UpdateTeacherResponse")]
        HttpClient.SchoolServiceRef.Teacher UpdateTeacher(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/UpdateTeacher", ReplyAction="http://tempuri.org/ISchoolService/UpdateTeacherResponse")]
        System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Teacher> UpdateTeacherAsync(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetPeople", ReplyAction="http://tempuri.org/ISchoolService/GetPeopleResponse")]
        HttpClient.SchoolServiceRef.PersonList GetPeople(string lastName, string firstName);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISchoolService/GetPeople", ReplyAction="http://tempuri.org/ISchoolService/GetPeopleResponse")]
        System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.PersonList> GetPeopleAsync(string lastName, string firstName);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISchoolServiceChannel : HttpClient.SchoolServiceRef.ISchoolService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SchoolServiceClient : System.ServiceModel.ClientBase<HttpClient.SchoolServiceRef.ISchoolService>, HttpClient.SchoolServiceRef.ISchoolService {
        
        public SchoolServiceClient() {
        }
        
        public SchoolServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SchoolServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SchoolServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SchoolServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public HttpClient.SchoolServiceRef.Student AddStudent(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa) {
            return base.Channel.AddStudent(id, lastName, firstName, dob, gender, major, units, gpa);
        }
        
        public System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Student> AddStudentAsync(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa) {
            return base.Channel.AddStudentAsync(id, lastName, firstName, dob, gender, major, units, gpa);
        }
        
        public void DeleteStudent(string id) {
            base.Channel.DeleteStudent(id);
        }
        
        public System.Threading.Tasks.Task DeleteStudentAsync(string id) {
            return base.Channel.DeleteStudentAsync(id);
        }
        
        public HttpClient.SchoolServiceRef.Student GetStudent(string id) {
            return base.Channel.GetStudent(id);
        }
        
        public System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Student> GetStudentAsync(string id) {
            return base.Channel.GetStudentAsync(id);
        }
        
        public System.Collections.Generic.List<HttpClient.SchoolServiceRef.Student> GetStudents() {
            return base.Channel.GetStudents();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<HttpClient.SchoolServiceRef.Student>> GetStudentsAsync() {
            return base.Channel.GetStudentsAsync();
        }
        
        public HttpClient.SchoolServiceRef.Student UpdateStudent(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa) {
            return base.Channel.UpdateStudent(id, lastName, firstName, dob, gender, major, units, gpa);
        }
        
        public System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Student> UpdateStudentAsync(string id, string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, string major, float units, float gpa) {
            return base.Channel.UpdateStudentAsync(id, lastName, firstName, dob, gender, major, units, gpa);
        }
        
        public HttpClient.SchoolServiceRef.Teacher AddTeacher(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary) {
            return base.Channel.AddTeacher(lastName, firstName, dob, gender, id, dateOfHire, salary);
        }
        
        public System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Teacher> AddTeacherAsync(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary) {
            return base.Channel.AddTeacherAsync(lastName, firstName, dob, gender, id, dateOfHire, salary);
        }
        
        public void DeleteTeacher(int id) {
            base.Channel.DeleteTeacher(id);
        }
        
        public System.Threading.Tasks.Task DeleteTeacherAsync(int id) {
            return base.Channel.DeleteTeacherAsync(id);
        }
        
        public HttpClient.SchoolServiceRef.Teacher GetTeacher(int id) {
            return base.Channel.GetTeacher(id);
        }
        
        public System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Teacher> GetTeacherAsync(int id) {
            return base.Channel.GetTeacherAsync(id);
        }
        
        public System.Collections.Generic.List<HttpClient.SchoolServiceRef.Teacher> GetTeachers() {
            return base.Channel.GetTeachers();
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<HttpClient.SchoolServiceRef.Teacher>> GetTeachersAsync() {
            return base.Channel.GetTeachersAsync();
        }
        
        public HttpClient.SchoolServiceRef.Teacher UpdateTeacher(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary) {
            return base.Channel.UpdateTeacher(lastName, firstName, dob, gender, id, dateOfHire, salary);
        }
        
        public System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.Teacher> UpdateTeacherAsync(string lastName, string firstName, System.DateTime dob, HttpClient.SchoolServiceRef.GenderEnum gender, int id, System.DateTime dateOfHire, int salary) {
            return base.Channel.UpdateTeacherAsync(lastName, firstName, dob, gender, id, dateOfHire, salary);
        }
        
        public HttpClient.SchoolServiceRef.PersonList GetPeople(string lastName, string firstName) {
            return base.Channel.GetPeople(lastName, firstName);
        }
        
        public System.Threading.Tasks.Task<HttpClient.SchoolServiceRef.PersonList> GetPeopleAsync(string lastName, string firstName) {
            return base.Channel.GetPeopleAsync(lastName, firstName);
        }
    }
}
