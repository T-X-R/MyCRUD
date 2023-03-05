import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Employee } from 'src/app/models/employee.model';
import { EmployeesService } from 'src/app/services/employees.service';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css'],
})
export class EditEmployeeComponent implements OnInit {
  employeeDetails: Employee = {
    id: '',
    name: '',
    email: '',
    phone: 0,
    salary: 0,
    department: '',
  };

  constructor(
    private route: ActivatedRoute,
    private employeeService: EmployeesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.route.paramMap.subscribe({
      next: (params) => {
        const id = params.get('id');
        if (id) {
          console.log(id);
          // get a single employee by id
          this.employeeService.getEmployee(id).subscribe({
            next: (res) => {
              this.employeeDetails = res;
            },
            error: (e) => {
              console.log(e);
            },
          });
        }
      },
    });
  }

  updateEmployee() {
    this.employeeService
      .updateEmployee(this.employeeDetails.id, this.employeeDetails)
      .subscribe({
        next: (res) => {
          this.router.navigate(['employees']);
        },
        error: (e) => {
          console.log(e);
        },
      });
  }

  deleteEmployee(id: string) {
    this.employeeService.deleteEmployee(id).subscribe({
      next: (res) => {
        this.router.navigate(['employees']);
      },
      error: (e) => {
        console.log(e);
      },
    });
  }
}
