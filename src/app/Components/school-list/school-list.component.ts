import { Component, OnInit } from '@angular/core';
import { SchoolService } from '../../Services/school.service';
import { School } from '../../Models/school';
import { CommonModule } from '@angular/common';
import { Grade } from '../../Models/grade';
import { Class } from '../../Models/class';
import { Student } from '../../Models/student';
import { FormsModule } from '@angular/forms';
import * as XLSX from 'xlsx';
import { jsPDF } from 'jspdf';
import html2canvas from 'html2canvas';
import { SchoolHeadrs } from '../../Models/school-headrs';
import { Year } from '../../Models/year';
import { ClassServiceService } from '../../Services/class-service.service';

@Component({
  selector: 'app-school-list',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './school-list.component.html',
  styleUrl: './school-list.component.css'
})
export class SchoolListComponent implements OnInit{
  schools:School[]=[];
  Years:Year[]=[];
  grades: Grade[]=[];
  classes: Class[]=[];
  Students: Student[]=[];
  reportHeaders:SchoolHeadrs|null=null;
  selectedSchoolId: number| null = null;
  selectedYear: number| null = null;
  selectedGrade: number| null = null;
  selectedClass: number | null = null;
  selectedClassName: string="";


  reportData: any = {}
  currentDate = new Date().toLocaleDateString(undefined, {
    weekday: 'long',
    month: 'long',
    day: 'numeric',
    year: 'numeric'
  });
  constructor(public schoolService:SchoolService , public classService:ClassServiceService){}
  ngOnInit(): void {

    this.schoolService.getAll().subscribe(data=>{
      console.log(data);
      this.schools=data;


    });
  }


  onSchoolSelect() {
    if(this.selectedSchoolId)
    this.schoolService.getYears(this.selectedSchoolId).subscribe(years => {
      console.log(years)
      this.Years = years;
      this.selectedYear = null;
      this.selectedGrade = null;
      this.selectedClass=null
      this.classes = [];

    });
  }
  onYearSelect() {
    if(this.selectedSchoolId &&this.selectedYear)
    this.schoolService.getGrades(this.selectedSchoolId, this.selectedYear).subscribe(grades => {
      this.grades = grades;
      this.selectedGrade = null;
      this.classes = [];
    });
  }

  onGradeChange(): void {
    if(this.selectedSchoolId &&this.selectedYear&&this.selectedGrade)
    this.schoolService.getClasses(this.selectedSchoolId, this.selectedYear, this.selectedGrade).subscribe(classes => {
      this.classes = classes;

    });
  }


  onClassChange(): void {
    if(this.selectedClass)
    this.classService.getClassById(this.selectedClass).subscribe(
      (clas: Class) => {
        this.selectedClassName = clas.name;
        console.log(this.selectedClassName)
      },

    );
    console.log('Selected Class ID:', this.selectedClass);
  }

 getReport(): void {

  if (!this.selectedSchoolId || !this.selectedYear || !this.selectedGrade || !this.selectedClass) {
    console.error('All dropdown selections are required.');
    return;
  }

  this.schoolService.getSchoolHeadersById(this.selectedSchoolId).subscribe({
    next: (headers) => {
      this.reportHeaders = headers;
    },
    error: (err) => {
      console.error('Error fetching school headers:', err);
      this.reportHeaders = null;
    }
  });

  this.schoolService.getReport(
    this.selectedSchoolId,
    this.selectedYear,
    this.selectedGrade,
    this.selectedClass
  ).subscribe(
    (data) => {
      this.reportData.Students = data.length ? data : [];
    },
  );
}


exportToExcel() {

  const ws: XLSX.WorkSheet = XLSX.utils.json_to_sheet(this.reportData.Students);


  const wb: XLSX.WorkBook = XLSX.utils.book_new();


  XLSX.utils.book_append_sheet(wb, ws, 'Students Report');


  XLSX.writeFile(wb, 'Students_Report.xlsx');
}


exportToPDF(): void {
  const data = document.getElementById('StudentTable');

  if (data) {
    html2canvas(data).then(canvas => {
      const imgWidth = 208;
      const pageHeight = 295;
      const imgHeight = (canvas.height * imgWidth) / canvas.width;

      const contentDataURL = canvas.toDataURL('image/png');
      const pdf = new jsPDF('p', 'mm', 'a4');

      let position = 0;
      pdf.addImage(contentDataURL, 'PNG', 0, position, imgWidth, imgHeight);
      pdf.save('StudentReport.pdf');
    });
  } else {
    console.error('Element with ID "StudentTable" not found.');
  }
}

printTable(): void {
  setTimeout(() => {
    const printContent = document.getElementById('StudentTable');
    const headerContent = document.querySelector('.Header');
    const headerContentArabic = document.querySelector('.Headertwo');
    const reportContentTable = document.querySelector('.reportTable');
    const returnImage = document.querySelector('.HeaderOne img');

    if (printContent && headerContent && reportContentTable && headerContentArabic && returnImage) {
      const clonedTable = printContent.outerHTML;
      const clonedHeader = headerContent.outerHTML;
      const clonedHeaderara = headerContentArabic.outerHTML;
      const clonedReportTable = reportContentTable.outerHTML;
      const clonedImage = returnImage.outerHTML;
   console.log(clonedImage)
      const printWindow = window.open('', '', 'left=0,top=0,width=900,height=900,toolbar=0,scrollbars=0,status=0');

      if (printWindow) {
        printWindow.document.write(`
          <html>
            <head>
              <title>Student Table</title>
              <style>
                body {
                  font-family: Arial, sans-serif;
                  margin: 20px;
                }
                table {
                  border-collapse: collapse;
                  width: 100%;
                  margin: 20px 0;
                }
                th, td {
                  border: 1px solid black;
                  padding: 8px;
                  text-align: left;
                }
                th {
                  background-color: #f2f2f2;
                }
                tr:nth-child(even) {
                  background-color: #f9f9f9;
                }
                 .text-right {
                  text-align: right !important;
                }
                .header-container {
                  display: flex;
                  justify-content: space-between;
                }
                .print-only {
                  display: block !important;
                }
                img {
                    display: block !important;
                    }
              </style>
            </head>
            <body class="rtl">
              <div class="header-container">
                ${clonedHeader} <!-- Left-aligned header -->
                ${clonedImage} <!-- Include return image -->
                ${clonedHeaderara} <!-- Right-aligned Arabic header -->
              </div>
              ${clonedReportTable} <!-- Include report table -->
              ${clonedTable} <!-- Include student table -->
            </body>
          </html>
        `);

        printWindow.document.close();
        printWindow.focus();
        printWindow.print();
        printWindow.close();
      }



      else {
        alert('Popup blocked! Please allow popups for this website.');
      }
    } else {
      alert('Table content not found. Please try again.');
    }
  }, 0);
}

}





