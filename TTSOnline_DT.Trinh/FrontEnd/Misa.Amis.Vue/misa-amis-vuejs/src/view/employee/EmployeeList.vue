<template>
  <div class="employee-list">
    <div class="content">
      <div class="content-title">
        <div class="content-title-text">Nhân viên</div>
        <BaseButtonIcon
          @btn-click="btnAddOnClick"
          subClass="btn-new"
          buttonIcon="button-icon"
          id="btnAdd"
          buttonText="Thêm mới nhân viên"
        />
      </div>
      <div class="filter">
        <BaseButtonIcon
        subClass="btn-delete"
        id="btnDelete"
        @btn-click="btnShowPopup"
        v-if="isChecked.includes(true)"
        />
        <BaseInputIcon
          nameClassInput="filter-search"
          typeName="text"
          detailSearch="Tìm theo mã, tên nhân viên"
          subClass="icon-search input-search"
          nameInput="inputSearch"
          idInput="txtSearch"
          iconClass="fas fa-times"
          :isHide="isHideBtnReset"
          @btnResetInput="btnResetInput"
          ref="inputSearchBox"
          v-model="keySearch"
          @search="reloadFilterPaging"
        />
       <BaseButtonIcon
          subClass="button-refresh"
          id="btnRefresh"
          @btn-click="btnRefreshOnClick"
        />
        <BaseButtonIcon
          subClass="button-export"
          id="btnexport"
          @btn-click="btnExportData"
        />
      </div>
      <div class="content-table">
        <table
          class="table table-striped"
          style="width: 100%"
          id="tbListDataEmployee"
        >
          <thead>
            <tr data-index="EmployeeId">
              <th style="padding-left: 8px">
                 <div
                  class="checkbox"
                  :class="{ 'checked icon-tick': isChecked[index] }"
                  @click="btnCheckedOnClick(index)"
                ></div>
              </th>
              <th scope="col" style="width: 40px" class="text-align-center" prop-name="EmployeeCode">MÃ NHÂN VIÊN</th>
              <th scope="col" prop-name="FullName">TÊN NHÂN VIÊN</th>
              <th scope="col" prop-name="GenderName">GIỚI TÍNH</th>
              <th
                scope="col"
                prop-name="DateOfBirth"
                format="ddmmyyyy"
                text-align="center"
                class="text-align-center">
                NGÀY SINH
              </th>
              <th scope="col" prop-name="CMND">SỐ CMND</th>
              <th scope="col" prop-name="PositionName">CHỨC DANH</th>
              <th scope="col" prop-name="DepartmentName">TÊN ĐƠN VỊ</th>
              <th scope="col" prop-name="BankAccount">SỐ TÀI KHOẢN</th>
              <th scope="col" prop-name="BankName">TÊN NGÂN HÀNG</th>
              <th scope="col" prop-name="BankBranch">CHI NHÁNH TK NGÂN HÀNG</th>
              <th scope="col" prop-name="Function">CHỨC NĂNG</th>
            </tr>
          </thead>
          <tbody>
            <tr
              v-for="(employee, index) in employees"
              :key="employee.EmployeeId"
              @dblclick="trOnDblClick(employee.EmployeeId)"
              v-bind:class="{
                wasChecked: isChecked[index],
              }"
            >
              <td>
                <div
                  class="checkbox"
                  :class="{ 'checked icon-tick': isChecked[index] }"
                  @click="btnCheckedOnClick(index)"
                ></div>
              </td>
              <td>{{ employee.EmployeeCode }}</td>
              <td class="width-150px">{{ employee.EmployeeName }}</td>
               <td >
                {{ employee.GenderName }}
              </td>
              <td class="text-align-center"> {{ formatDateDMY(employee.DateOfBirth) }}</td>
              <td class="width-170px">{{ employee.IdentityNumber }}</td>
              <td class="width-150px">{{ employee.EmployeePosition }}</td>
              <td>{{ employee.DepartmentName }}</td>
              <td>{{ employee.BankAccountNumber }}</td>
              <td>{{ employee.BankName }}</td>
              <td>{{ employee.BankBranchName }}</td>
              <td>
                <div
                  class="checkbox"
                  :class="{ 'checked icon-tick': isChecked[index] }"
                  @click="btnCheckedOnClick(index)"
                >
                <span style="margin-left:23px">Xóa</span>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </div>
      <BasePaging
        :totalEmployee="totalEmployee"
        :totalButton="totalButton"
        @UpdatePage="UpdatePage"
      />
    </div>

    <BaseLoading :HideLoader="HideLoader" />

    <BaseMessage
      :isHide="isHideMessage"
      :errors="errors"
      @closeMsg="closeMsg"
    />
    <EmployeeDetail
      :isHide="isHideDialogDetail"
      @btnAddOnClick="btnAddOnClick"
      @btnCancelOnClick="btnCancelOnClick"
      :formMode="dialogFormMode"
      @loadData="loadData"
      :employeeId="employeeId"
      :employeeCode="employeeCode"
      @showMsgMisRequied="showMsgMisRequied"
      @showMsgEditSuccess="showMsgEditSuccess"
      @showMsgAddSuccess="showMsgAddSuccess"
    />
  </div>
</template>
<script>
import axios from "axios";
import https from "https";
import dayjs from "dayjs";
import BaseButtonIcon from "../../components/base/BaseButtonIcon.vue";
// import Dropdown from "../../components/base/Dropdown.vue";
import BaseLoading from "../../components/base/BaseLoading.vue";
import BaseInputIcon from "../../components/base/BaseInputIcon.vue";
// import BasePopup from "../../components/base/BasePopup.vue";
import BaseMessage from "../../components/base/BaseMessage.vue";
import BasePaging from "../../components/base/BasePaging.vue";
import EmployeeDetail from "../../view/employee/EmployeeDetail.vue";
import message from "../../js/common/message.js";
import type from "../../js/common/type.js";
import { eventBus } from "../../main.js";
// import $ from 'jquery'
// import common from "../../js/common/common.js";

// import VueAxios from 'vue-axios'
export default {
  name: "EmployeeList",
  components: {
    BaseButtonIcon,
    // Dropdown,
    BaseInputIcon,
    EmployeeDetail,
    BaseMessage,
    BasePaging,
    BaseLoading,
  },
  created() {
    this.loadData();
    this.loadDataFilter();
    eventBus.$on("btnDeleteOnClick", () => {
      this.btnDeleteOnClick();
    });
  },
  data() {
    return {
      employees: [],
      keySearch: "",
      formMode: "",
      dialogFormMode: "",
      isHideDialogDetail: true,
      employeeId: "",
      employeeCode: "",
      isChecked: [],
      isHideMessage: true,
      errors: [],
      isHideBtnReset: true,
      totalEmployee: 0,
      currentIndex: 1,
      EmployeePerPage: 10,
      totalButton: 1,
      HideLoader: true,
    };
    
  },
  watch: {},
  methods: {
    /**
     * Hàm Ẩn/hiện Loader
     * createdBy: DT.Trinh
     */
    RefreshOnClick() {
      this.HideLoader = false;
      setTimeout(() => {
        this.HideLoader = true;
      }, 2000);
    },

    /**
     * Hàm cập nhật các bản ghi từng trang
     * createdBy: DT.Trinh
     */
    UpdatePage(currentIndex, EmployeePerPage) {
      let me = this;
      me.currentIndex = currentIndex;
      me.EmployeePerPage = EmployeePerPage;
      me.loadData();
      me.RefreshOnClick();
      console.log(currentIndex, EmployeePerPage);
    },

    /**
     * Hàm load dữ liệu từ API để hiển thị vào bảng
     * CreateBy DT.Trinh
     */
    loadData() {
      // require('axios').globalAgent.options.ca = require('ssl-root-cas').create();
      let me = this;
      const agent = new https.Agent({  
      rejectUnauthorized: false
      })
      axios
        .get("https://localhost:44331/api/v1/Employees", { httpsAgent: agent })
        .then((res) => {
          me.employees = res.data.slice(
            (me.currentIndex - 1) * me.EmployeePerPage,
            (me.currentIndex - 1) * me.EmployeePerPage + me.EmployeePerPage
          );
          me.isChecked = new Array(me.employees.length).fill(false);
          me.totalEmployee = res.data.length;
          me.totalButton = Math.ceil(res.data.length / me.EmployeePerPage);
        })
        .catch((res) => {
          console.log(res);
          this.showMessage(type.WARNING, [message.EXCEPTION_MSG]);
        });
    },

    /**
     * Hàm click chọn một bản ghi
     * CreateBy DT.Trinh
     */
    getEmployeeId(employeeId) {
      this.employeeId = employeeId;
      this.wasChecked = employeeId;
      this.dialogFormMode = "del";
    },

    /**
     * Hàm hiển thị popup
     * CreateBy DT.Trinh
     */
    btnShowPopup() {
      eventBus.$emit("showPopup");
    },

    /**
     * Hàm refesh 
     *  CreateBy DT.Trinh
     */
    btnRefreshOnClick() {
      this.loadData();
      this.RefreshOnClick();
      this.showMessage(type.SUCCESS, [message.REFRESH_MSG_SUCCESS]);
    },

    /**
     * Hàm thêm mới nhân viên
     * CreateBy DT.Trinh
     */
    btnAddOnClick() {
      this.isHideDialogDetail = false;
      this.dialogFormMode = "add";
    },

    /**
     * Hàm cho button hủy, ẩn dialog thêm nhân viên
     * CreateBy DT.Trinh
     */
    btnCancelOnClick() {
      this.isHideDialogDetail = true;
      this.formMode = "";
    },

    /**
     * Hàm format date sang dạng dd/mm/yyyy
     * CreateBy DT.Trinh
     */
    formatDateDMY(date) {
      if (date == null) {
        return "";
      }
      return dayjs(date).format("DD/MM/YYYY");
    },
  
    /**
     * Hàm hiển thị dialog
     * CreateBy DT.Trinh
     */
    trOnDblClick(employeeId) {
      this.dialogFormMode = "edit";
      //Hien thi Dialog
      this.isHideDialogDetail = false;
      this.employeeId = employeeId;
    },

    /**
     * Hàm xóa nhân viên
     * CreateBy DT.Trinh
     */
    btnDeleteOnClick() {
      for (var i = 0; i < this.employees.length; i++) {
        if (this.isChecked[i]) {
          axios
            .delete(
              "https://localhost:44331/api/v1/Employees/" +
                this.employees[i].EmployeeId
            )
            .then((res) => {
              console.log("res" + res);
              this.showMessage(type.SUCCESS, [message.DELETE_MSG_SUCCESS]);
              this.isHidePopup = true;
              this.loadData();
            })
            .catch((res) => {
              console.log(res);
              this.showMessage(type.WARNING, [message.EXCEPTION_MSG]);
            });
        }
      }
    },

    /**
     * Hàm đánh dấu bản ghi đã được chọn
     * CreateBy DT.Trinh
     */
    btnCheckedOnClick(index) {
      this.dialogFormMode = "del";
      this.$set(this.isChecked, index, !this.isChecked[index]);
      console.log(this.isChecked);
    },

    /**
     * Tìm kiếm nhân viên
     * creatBy: DT.Trinh
     */
    reloadFilterPaging() {
      this.loadDataFilter();
    },
    loadDataFilter() {
       let me = this;
      const agent = new https.Agent({  
      rejectUnauthorized: false
      })
      axios
        .get("https://localhost:44331/api/v1/Employees/Filter?KeySearch="+
            me.keySearch +
            "&pageSize=" +
            me.EmployeePerPage +
            "&pageIndex=" +
            me.currentIndex, { httpsAgent: agent })
        .then((res) => {
           if (res.status != 204) {
            me.employees = res.data.Data;
            me.totalButton = res.data.TotalPage;
            me.totalEmployee = res.data.TotalRecord;
          } else {
            me.employees = [];
            me.totalEmployee = 0;
            me.totalButton = 1;
          }
         
        })
        .catch((res) => {
          console.log(res);
        });
    },

    /**
     * Xuất file excel
     * CreateBy: DT.Trinh
     */
    btnExportData(){
       let me = this;
      const agent = new https.Agent({  
      rejectUnauthorized: false
      })
      axios
        .get("https://localhost:44331/api/v1/Employees/Export?KeySearch="+
            me.keySearch +
            "&pageSize=" +
            me.EmployeePerPage +
            "&pageIndex=" +
            me.currentIndex, { httpsAgent: agent })
        .then((res) => {
           if (res.status != 204) {
            const url = window.URL.createObjectURL(new Blob([res.data]));
            const a = document.createElement("a");
            a.href = url;
            const filename = `Danh_sach_nhan_vien.xlsx`;
            a.setAttribute("download", filename);
            document.body.appendChild(a);
            a.click();
            a.remove();
          } else {
            console.log("empty");
          }
        })
        .catch((res) => {
          console.log(res);
        });
    },


    /**
     * Đóng message thông báo
     *CreateBy DT.Trinh
     */
    closeMsg(value) {
      this.isHideMessage = value;
    },

    /**
     * Hiển thị message thông báo
     * CreateBy DT.Trinh
     */
    showMessage(status, array) {
      this.isHideMessage = false;
      if (array.length == 0) {
        this.errors[0].statusMsg = status;
        this.errors[0].titleMsg = message.ADD_UPDATE_MSG_NO_SUCCESS;
      } else {
        this.errors = array.map((item) => {
          return {
            statusMsg: status,
            titleMsg: item,
          };
        });
      }
      setTimeout(() => {
        this.isHideMessage = true;
      }, 2000);
    },

    /**
     * Hiển thị toastmess thêm mới thành công
     * CreateBy DT.Trinh
     */
    showMsgAddSuccess() {
      this.showMessage(type.SUCCESS, [message.ADD_MSG_SUCCESS]);
    },

    /**
     * Hiển thị toastmess sửa thành công
     * CreateBy DT.Trinh 
     */
    showMsgEditSuccess() {
      this.showMessage(type.SUCCESS, [message.EDIT_MSG_SUCCESS]);
    },

    /**
     * Hiển thị toastmess thông báo chưa nhập đủ các trường
     * CreateBy DT.Trinh 
     */
    showMsgMisRequied() {
      this.showMessage(type.WARNING, [message.VALIDATE_MSG_MIS_REQUIRED]);
    },
    
    /**
     * Hàm btn reset input
     * CreateBy DT.Trinh 
     */
    btnResetInput() {
      this.isHideBtnReset = true;
    },
  },
};
</script>
<style>
@import "../../css/layout/content.css";
@import "../../css/base/table.css";

.wasChecked {
  background-color: #ebf9f4 !important;
}

</style>