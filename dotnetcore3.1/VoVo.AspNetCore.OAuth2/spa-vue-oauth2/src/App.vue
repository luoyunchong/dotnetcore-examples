<template>
  <div id="app">
    <img alt="Vue logo" src="./assets/logo.png" />
    <!-- <el-button type="primary" @click="github">GitHub登录</el-button> -->
    <el-button type="primary" @click="signin">GitHub登录</el-button>
  </div>
</template>

<script>
export default {
  name: "app",
  components: {},
  methods: {
    github() {
      window.open(
        "https://github.com/login/oauth/authorize?client_id=6daf87d8c206b823763a&redirect_uri=http://localhost:8080/login-result"
      );
    },
    signin() {
      window.open("https://localhost:5001/signin?provider=GitHub");
    }
  }
};
</script>

<style>
#app {
  font-family: "Avenir", Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>
