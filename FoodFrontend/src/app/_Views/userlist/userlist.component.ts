import { Component, OnInit, ViewChild } from '@angular/core';
import { AbstractControl, FormBuilder, FormControl, FormGroup, ValidatorFn, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { faPencilAlt, faPencilRuler, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { ApiDataService } from 'src/app/_Services/api-data.service';
import ValidateForm from 'src/app/_helper/validate-form';

@Component({
  selector: 'app-userlist',
  templateUrl: './userlist.component.html',
  styleUrls: ['./userlist.component.css']
})
export class UserlistComponent implements OnInit {
  minDate = new Date(1900, 0, 1);
  maxDate =  new Date(new Date().setDate(new Date().getDate()-1))

  addUserForm: FormGroup;
  userList : any[] = [];
  loading = false;
  edit = faPencilAlt;
  delete = faTrashAlt;
  editModal : boolean = false;
  showPassDiv: boolean = true;

  constructor(private _service: ApiDataService, private builder: FormBuilder, private toast: ToastrService) { }
  displayedColumns : string[] = ['position','FirstName', 'LastName', 'Email', 'PhoneNumber', 'Status', 'Actions']
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator !: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  ngOnInit(): void {
   //this.showPassDiv = false
    this.GetAllUser();
    this.addUserForm = this.builder.group({
      firstName: ['',Validators.compose([Validators.required, Validators.minLength(4)])],
      lastName:['',Validators.compose([Validators.required, Validators.maxLength(12)])],
      email: ['',Validators.compose([Validators.required,Validators.email])],
      phoneNumber: ['',Validators.compose([Validators.required, Validators.pattern('^[0-9]{10,10}$')])],
      dateofBirth: [new Date()],
      enabled: [true],
      password: ['',Validators.compose([Validators.required, Validators.pattern('(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[$@$!%*?&])[A-Za-z\d$@$!%*?&].{8,12}')])],
      confirmPassword: ['',Validators.required],
    },{validators: [this.match('password','confirmPassword')]})
  }

  GetAllUser(){
    try{
      this.loading = true;
      this._service.getAllUsers().subscribe((data) => {
        console.log(data);
        this.userList = data;
        this.dataSource = new MatTableDataSource(this.userList);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
        this.loading = false;
      })
    }
    catch(err){
      console.log(err);
    }
   
  }

  addUser(){
    this.showPassDiv = true;
    var node = document.getElementsByClassName('container') as HTMLCollectionOf<HTMLElement>;
      for (let i = 0; i < node.length; i++){
        node[i].style.display = 'block';
      }
  }
  checkPass(){
    const pass = document.getElementById('password') as HTMLInputElement;
    const confirm = document.getElementById('confirm_password')as HTMLInputElement;
    if(confirm!.value === pass!.value){
       confirm.setCustomValidity('Password is matched')
    }
    else{
      confirm.setCustomValidity('Password not matched');
    }
  }

  onSubmit(){
    if(this.addUserForm.valid){
      console.log(this.addUserForm.value);
      this.checkPass();
      let addUserObj = this.addUserForm.value;
      this._service.saveUser(addUserObj).subscribe({
        next: (res => {
          console.log(res);
          this.addUserForm.reset();
          this.resetForm();
          this.GetAllUser();
          this.toast.success('User added successfully', 'Success!',{
            timeOut: 5000
          })
        }),
        error: (err)=> {
          this.toast.error('Somethng went wrong', 'Error!', {
            timeOut: 5000
          });
          console.log(err);
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.addUserForm);
    }
  }

  resetForm(){
     this.addUserForm.reset();
     var node = Array.from(document.getElementsByClassName('container') as HTMLCollectionOf<HTMLElement>);
     node.forEach(element => {
      element.style.display = 'none';
     });
  }

   match(controlName: string, checkControlName: string): ValidatorFn {
    return (controls: AbstractControl) => {
      const control = controls.get(controlName);
      const checkControl = controls.get(checkControlName);

      if (checkControl?.errors && !checkControl.errors['matching']) {
        return null;
      }

      if (control?.value !== checkControl?.value) {
        controls.get(checkControlName)?.setErrors({ matching: true });
        return { matching: true };
      } else {
        return null;
      }
    };
  }

  editUser(id : number){
    this.showPassDiv = false;
    this.editModal = true;
    console.log(id);
    var user = this.userList.find(x => x.Id == id);
    this.addUserForm.patchValue({
      Id: user.Id,
      firstName: user.FirstName,
      lastName: user.LastName,
      email: user.Email,
      phoneNumber: user.PhoneNumber,
      dateOfBirth: user.DateOFBirth,
      //password: user.Password,
      //confirmPassword: user.Password
    })
    this.addUser();
  }

  deleteUser(id : number){
    console.log(id)
    this._service.deleteUser(id).subscribe(d => {
      this.GetAllUser();
      this.toast.success('User removed successfully!')
    })
  }
}
