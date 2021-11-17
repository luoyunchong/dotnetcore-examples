<template>
  <div class="main">
    <h2>Login-Result</h2>
    <p>
      （浏览器直接打开这个地址能得到github的id ）
      <a
        :href="config.baseUrl+'OpenId?provider=GitHub'"
      >{{config.baseUrl+"OpenId?provider=GitHub"}}</a>
    </p>
    <p>但用axios带token获取 OpenId:{{OpenId1}}</p>
    <p>GetOpenIdByToken:{{OpenId2}}</p>
    <el-alert title="登录成功" type="info"></el-alert>
  </div>
</template>

<script>
const axios = require('axios');
function parseUrlParams() {
  if (window.location.search.length <= 0) return false;
  var info = window.location.search.slice(1);
  var result = {};
  info.split('&').forEach(item => {
    result[decodeURIComponent(item.split('=')[0])] = decodeURIComponent(
      item.split('=')[1]
    );
  });
  return result;
}

import config from '../config/index';
export default {
  name: 'LoginResult',
  props: {},
  data() {
    return {
      config: config,
      OpenId1: '',
      OpenId2: ''
    };
  },
  created() {
    var result = parseUrlParams();
    if (!(result && result.token)) {
      alert('无效的登录');
      return;
    }
    var that = this;
    axios({
      methods: 'get',
      url: 'OpenId?provider=GitHub',
      headers: {
        Authorization: 'Bearer ' + result.token
      }
    }).then(function(response) {
      console.log(response);
      that.OpenId1 = response.data;
    });

    axios({
      methods: 'get',
      url: config.baseUrl + 'GetOpenIdByToken',
      headers: {
        Authorization: 'Bearer ' + result.token
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
