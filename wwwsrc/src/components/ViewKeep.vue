<template>
    <v-dialog v-model="viewKeep" transition="scale-transition" width="fit-content">
        <v-btn @click="viewed()" slot="activator" flat fab small color="amber darken-4">view</v-btn>
        <v-layout justify-center>
            <v-card v-if="keep.img.length > 0" width="80vw" height="80vh">
                <v-toolbar card color="cyan lighten-4">
                    <v-layout column fill-height justify-center align-start>
                        <v-btn flat small>{{author.username}}</v-btn>
                        <v-card-title class="img-title">{{keep.name}}</v-card-title>
                    </v-layout>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-speed-dial v-model="shareExpand" direction="left" transition="slide-x-reverse-transition">
                            <v-btn slot="activator" v-model="shareExpand" color="amber darken-3" fab>
                                <v-icon>fa-share</v-icon>
                                <v-icon>close</v-icon>
                            </v-btn>
                            <v-btn v-if="keep.img.length == 0" fab small color="black" class="white--text">
                                <v-icon>fab fa-twitter</v-icon>
                            </v-btn>
                            <v-btn v-else fab small color="#e62774">
                                <v-icon>fab fa-instagram</v-icon>
                            </v-btn>
                            <v-btn fab small color="blue">
                                <v-icon>fab fa-facebook-f</v-icon>
                            </v-btn>
                        </v-speed-dial>
                        <v-chip outline color="amber darken-4">
                            {{keep.views}}<v-icon right>fa-eye</v-icon>
                        </v-chip>
                    </v-toolbar-items>
                </v-toolbar>
                <v-img v-if="keep.img.length > 0" :src="keep.img" class="white--text" width="100%" height="100%"></v-img>
            </v-card>
            <v-card v-else width="40vw" height="fit-content">
                <v-toolbar card color="cyan lighten-4">
                    <v-layout column fill-height justify-center align-start>
                        <v-btn flat small>{{author.username}}</v-btn>
                        <v-card-title class="img-title">{{keep.name}}</v-card-title>
                    </v-layout>
                    <v-spacer></v-spacer>
                    <v-toolbar-items>
                        <v-speed-dial v-model="shareExpand" direction="left" transition="slide-x-reverse-transition">
                            <v-btn slot="activator" v-model="shareExpand" color="amber darken-3" fab>
                                <v-icon>fa-share</v-icon>
                                <v-icon>close</v-icon>
                            </v-btn>
                            <v-btn v-if="keep.img.length == 0" fab small color="black" class="white--text">
                                <v-icon>fab fa-twitter</v-icon>
                            </v-btn>
                            <v-btn v-else fab small color="#e62774">
                                <v-icon>fab fa-instagram</v-icon>
                            </v-btn>
                            <v-btn fab small color="blue">
                                <v-icon>fab fa-facebook-f</v-icon>
                            </v-btn>
                        </v-speed-dial>
                        <v-chip outline color="amber darken-4">
                            {{keep.views}}<v-icon right>fa-eye</v-icon>
                        </v-chip>
                    </v-toolbar-items>
                </v-toolbar>
            </v-card>
        </v-layout>
    </v-dialog>
</template>
<script>
export default {
  name: "viewKeep",
  data() {
    return {
      viewKeep: false,
      shareExpand: false
    };
  },
  props: ["keep"],
  computed: {
    author() {
      return this.$store.state.author;
    }
  },
  methods: {
    viewed() {
      this.$store.dispatch("getAuthor", this.keep.userId);
      this.$store.dispatch("incrementViews", this.keep.id);
      this.keep.views++;
    }
  }
};
</script>