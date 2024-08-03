import { validateVerticalPosition } from '@angular/cdk/overlay';
import { HttpErrorResponse, HttpEventType, HttpResponse } from '@angular/common/http';
import { Component, ElementRef, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, NgModel, Validators } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { faEdit, faPlus, faTrash } from '@fortawesome/free-solid-svg-icons';
import { ToastrService } from 'ngx-toastr';
import { catchError, map, of } from 'rxjs';
import { ApiDataService } from 'src/app/_Services/api-data.service';
import ValidateForm from 'src/app/_helper/validate-form';
import {MatDialog} from '@angular/material/dialog'
import { Products } from 'src/app/_Models/products';


@Component({
  selector: 'app-admin',
  templateUrl: './admin.component.html',
  styleUrls: ['./admin.component.css'],
  providers: [NgModel]
})
export class AdminComponent implements OnInit {
  public productForm!: FormGroup;
 productList : any[] = [];
 edit = faEdit;
 delete = faTrash;
 plus = faPlus;
 productImage: any = '../assets/Images/Default.avif';
 loading = false;
  file: File;
  progressValue = 0;
  editModal: boolean = false;
  editProductML: Products = new Products();
  constructor(private dataService : ApiDataService, private fb: FormBuilder, private toastr: ToastrService, public dialog: MatDialog) { }
  displayedColumns: string[] = ['Position','ProductName','ProductCode', 'description', 'Actions'];
  dataSource: MatTableDataSource<any>;
  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort : MatSort;
  @ViewChild('dialogContent') addView !: TemplateRef<any>;
  @ViewChild('fileupload') fileupload !: ElementRef;
  

  ngOnInit(): void {
    this.GetAllProduct();
    this.productForm = this.fb.group({
      //Id: ['0'],  //this id field is used for update process
      Name: ['',Validators.required],
      UnitPrice: ['',Validators.required],
      //Url: ['', Validators.required],
      Description: ['',Validators.required],
      Enabled:[true, Validators.required],
      ImageUrl: ['', Validators.required]
    })
  }

  GetAllProduct(){
    this.loading = true;
    this.dataService.getAllProducts().subscribe((data) => {
      console.log(data);
      this.loading = false;
       this.productList = data;
       this.dataSource = new MatTableDataSource(this.productList);
       //this.paginator.pageSize = this.recordsSelected;
       this.dataSource.paginator = this.paginator;
       this.dataSource.sort = this.sort
    })
  }

  AddProduct(){
    let modalDiv = document.getElementById('productModal');
    if(modalDiv != null){
      modalDiv.style.display = 'block'
    }
  }

  CloseModal(){
    let modalDiv = document.getElementById('productModal');
    if(modalDiv != null){
      modalDiv.style.display = 'none';
    }
    this.productForm.reset();
  }

  onSubmit(){
    if(this.productForm.valid){
      console.log(this.productForm.value);
      this.dataService.saveProduct(this.productForm.getRawValue()).subscribe({
        next: (res => {
          console.log(res);
          //this.productForm.reset();
          this.CloseModal();
          this.toastr.success('Product saved successfully.', 'Successfull!',{
            timeOut: 5000
          })
          this.GetAllProduct()
        }),
        error: (err) => {
         this.toastr.error('Something went wrong', 'Error!',{
          timeOut: 5000
         });
         console.log(err);
        }
      })
    }
    else{
      ValidateForm.validateAllFormFields(this.productForm);
    }
  }

  GetImagePreview(item: any){
     if(item.target.files.length > 0){
      const reader = new FileReader();
      this.file = item.target.files[0];
       reader.readAsDataURL(this.file);
       reader.onload=()=>{
        this.productImage = reader.result;
       }
     }
  }

  UploadImage(){
    let formData = new FormData();
    formData.append('file', this.file,this.file.name);
    this.dataService.uploadImage(formData).pipe(
      map((events: any) => {
         let ele = document.getElementById('stripped');
         if(ele)
         ele.classList.remove('d-none');
        switch (events.type) {
          case HttpEventType.UploadProgress:
            this.progressValue = Math.round(events.loaded / events.total! * 100);
            break;
          case HttpEventType.Response:
           // this.Getallproducts();
           this.toastr.success('Upload Complete', 'Uploaded!')
            setTimeout(() => {
              this.progressValue = 0;
            }, 2500);
            break;

        }
      }),
      catchError((error: HttpErrorResponse) => {
        this.toastr.error('Failed to Upload', 'Failed!')
        return of("failed");
      })
    ).subscribe(res => {
      console.log(res);
    })
  }

  editProduct(id : any){
    debugger;
     console.log(id);
     this.editModal = true;
     var product = this.productList.find(u => u.Id == id);
     console.log(product);
     this.productForm.patchValue({
      Id: product.Id,
      Name: product.Name,
      unitPrice: product.UnitPrice,
      Url: null,
      Description: product.Description,
      Enabled:product.Enabled
     })
      this.AddProduct();
      console.log(this.productForm.value);
     
  }

  DeleteProduct(id: number){
     console.log(id);
     debugger;
     this.dataService.deleteProdut(id).subscribe((data) => {
      console.log('Record deleted');
      this.toastr.success('Record deleted', 'Deleted!',{
        timeOut: 5000
       });
     })
     
     
     
  }
  
  openDialog() : void{
    const dialogRef = this.dialog.open(this.addView, {width:'500px'});
    dialogRef.afterClosed().subscribe((data) => {
      console.log(data);
      window.location.reload();
    });
  }

  onNoClick(): void {
     this.dialog.closeAll();
  }
}
