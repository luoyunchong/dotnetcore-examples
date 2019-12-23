<template>
  <div class="main">Login-Result</div>
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
  created() {
    var result = parseUrlParams();
    if (!(result && result.token)) {
      alert("无效的登录");
      return;
    }

    axios({
      methods: "get",
      url: "https://localhost:5001/OpenId?provider=GitHub",
      headers: {
        Authorization: "Bearer " + result.token
      }
    })
      .then(function(response) {
        // handle success
        console.log(response);
      })
      .catch(function(error) {
        // handle error
        console.log(error);
      })
      .finally(function() {
        // always executed
      });

    axios({
      methods: "get",
      url: "https://localhost:5001/GetOpenIdByToken",
      headers: {
        Authorization: "Bearer " + result.token
      }
    })
      .then(function(response) {
        // handle success
        console.log(response);
      })
      .catch(function(error) {
        // handle error
        console.log(error);
      })
      .finally(function() {
        // always executed
      });
  }
};
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
</style>
