import { Component, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { ApiService } from 'src/app/Services/api.service';
export interface PeriodicElement {
  name: string;
  position: number;
  weight: number;
  symbol: string;
}
const ELEMENT_DATA: PeriodicElement[] = [
  { position: 1, name: 'Hydrogen', weight: 1.0079, symbol: 'H' },
  { position: 2, name: 'Helium', weight: 4.0026, symbol: 'He' },
  { position: 3, name: 'Lithium', weight: 6.941, symbol: 'Li' },
  { position: 4, name: 'Beryllium', weight: 9.0122, symbol: 'Be' },
  { position: 5, name: 'Boron', weight: 10.811, symbol: 'B' },
  { position: 6, name: 'Carbon', weight: 12.0107, symbol: 'C' },
  { position: 7, name: 'Nitrogen', weight: 14.0067, symbol: 'N' },
  { position: 8, name: 'Oxygen', weight: 15.9994, symbol: 'O' },
  { position: 9, name: 'Fluorine', weight: 18.9984, symbol: 'F' },
  { position: 10, name: 'Neon', weight: 20.1797, symbol: 'Ne' },
];
@Component({
  selector: 'app-product-management',
  templateUrl: './product-management.component.html',
  styleUrls: ['./product-management.component.css'],
})
export class ProductManagementComponent implements OnInit {

  productForm: FormGroup;
  editModal = false;
  loading  = false;
  constructor(private api: ApiService, private fb: FormBuilder, private toastr: ToastrService) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
        name: ['', [Validators.required, Validators.maxLength(15)]],
        title: ['', [Validators.required,Validators.maxLength(30)]],
        description: ['', [Validators.required, Validators.maxLength(50)]],
        quantity: ['', [Validators.required]],
        unitprice: ['', Validators.required],
        discount: ['', Validators.required],
    })
    this.getProdcutList();

  }

  displayedColumns: string[] = ['position', 'name', 'weight', 'symbol'];
  dataSource = new MatTableDataSource(ELEMENT_DATA);

  @ViewChild(MatSort) sort: MatSort;

  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  getProdcutList() {
    this.api.getProdcuts().subscribe((resp) => {
      console.log(resp);
    });
  }

  saveProduct(){
    this.loading = true
   console.log(this.productForm.value);
   if(this.productForm.valid){
      this.api.insertOrUpdateProduct(this.productForm.value).subscribe({
        next: (resp) => {
            if(resp.Success){
              this.toastr.success('Product saved successfully', "Success");
              this.productForm.reset();
              this.closeModal();
              this.loading = false;
            }
            else{
              this.toastr.error('Something went wrong', "Error!");
              this.loading = false;
            }
        },error: (err) => {
          console.log(err);
          this.loading = false;
        }
      })
   }
   else{
     this.toastr.error('Invalid Input', "Error!")
     this.loading = false
   }
  }

  closeModal(){
    document.getElementById('close-btn')?.click();
  }
}
