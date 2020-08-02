<template>
  <div class="main">
    <h2>Login-Result</h2>
    <p>OpenId:{{OpenId1}}</p>
    <p>GetOpenIdByToken{{OpenId2}}</p>
  </div>
</template>

<script>
const axios = require("axios");
function parseUrlParams() {
  if (window.location.search.length <= 0) return false;
  var info = window.location.search.slice(1);
  var result = {};
  info.split("&").forEach(item => {
    result[decodeURIComponent(item.split("=")[0])] = decodeURIComponent(
      item.split("=")[1]
    );
  });
  return result;
}

export default {
  name: "LoginResult",
  props: {},
  data() {
    return {
      OpenId1: "",
      OpenId2: ""
    };
  },
  created() {
    var result = parseUrlParams();
    if (!(result && result.token)) {
      alert("无效的登录");
      return;
    }
    var that = this;
    axios({
      methods: "get",
      url: "https://localhost:5001/OpenId?provider=GitHub",
      headers: {
        Authorization: "Bearer " + result.token
      }
    }).then(function(response) {
      console.log(response);
      that.OpenId1 = response.data;
    });

    axios({
      methods: "get",
      url: "https://localhost:5001/GetOpenIdByToken",
      headers: {
        Authorization: "Bearer " + result.token
      }
    }).then(function(response) {
      console.log(response);
      that.OpenId2 = response.data;
    });
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
