<template>
  <div class="home">
    <navigation-bar />
    <h1>Welcome Home</h1>
  </div>
</template>

<script>
import NavigationBar from "@/components/NavigationBar";
  export default {
    name: "home",
    mounted() {
      //blocks users not logged in
      if (!this.$store.state.user.id) {
        this.$router.push({ name: "login" });
      }
    },
    components: {
      NavigationBar
    }
  };
</script>