<template>
  <div id="app">
    <Header />
    <Menu />
    <Content />
    <BasePopup
      :isPopup="isHidePopup"
      @btnHidePopup="btnHidePopup"
      :titleText="titleText"
      :textMessDetail="textMessDetail"
      :type="type"
    />
  </div>
</template>

<script>
import Header from "./components/layout/TheHeader.vue";
import Menu from "./components/layout/TheMenu.vue";
import Content from "./components/layout/TheContent.vue";
import BasePopup from "./components/base/BasePopup.vue";
import { eventBus } from "./main.js";
export default {
  name: "App",
  components: {
    Header,
    Menu,
    Content,
    BasePopup,
  },
  data() {
    return {
      isHidePopup: true,
      titleText: "",
      textMessDetail: "",
      type: "",
    };
  },
  created() {
    eventBus.$on("showPopup", () => {
      this.type = "delete";
      this.textMessDetail = "Bạn chắc chắn muốn xóa nhân viên này không?";
      this.titleText = "Xóa";
      this.isHidePopup = false;
    });
    eventBus.$on("showPopupForSave", (mode) => {
      if (mode == "add") {
        this.textMessDetail = "Bạn có muốn thêm nhân viên mới?";
        this.titleText = "Thêm mới";
        this.type = mode;
      } else {
        this.textMessDetail = "Bạn có muốn cập nhật thông tin nhân viên?";
        this.titleText = "Cập nhật";
        this.type = mode;
      }
      this.isHidePopup = false;
    });
    eventBus.$on("showPopupForCancel", (mode)=>{
      this.type = mode;
      this.textMessDetail = "Bạn có chắc muốn đóng form?"
      this.titleText = "Đóng Form"
      this.isHidePopup = false;
    });
    eventBus.$on('hidePopup',()=>{
      this.btnHidePopup();
    })
  },
  methods: {

    btnHidePopup() {
      this.isHidePopup = true;
    },
  },
};
</script>
  
<style>
@import "./css/main.css";
</style>
